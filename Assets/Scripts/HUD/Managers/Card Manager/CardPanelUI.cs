using System.Collections.Generic;
using UnityEngine;

public class CardPanelUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] CardUI[] slots;

    private void Awake()
    {
        panel.SetActive(false);
    }

    public void Show(List<CardData> cards, System.Action<CardData> on_chosen)
    {
        panel.SetActive(true);

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < cards.Count)
            {
                slots[i].gameObject.SetActive(true);
                slots[i].Setup(cards[i], on_chosen);
            }
            else
            {
                slots[i].gameObject.SetActive(false);
            }
        }
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}