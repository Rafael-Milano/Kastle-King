using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] TMP_Text name_text;
    [SerializeField] TMP_Text description_text;
    [SerializeField] Image background;
    [SerializeField] Button button;
    [SerializeField] CardRarityStyle rarity_style;

    public void Setup(CardData card, System.Action<CardData> on_chosen)
    {
        name_text.text = card.card_name;
        description_text.text = card.Description;
        background.sprite = rarity_style.GetSprite(card.rarity);
        background.color = Color.white;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => on_chosen(card));
    }

    string RarityLabel(CardRarity rarity)
    {
        switch (rarity)
        {
            case CardRarity.Common: return "Comum";
            case CardRarity.Rare: return "Rara";
            case CardRarity.Enchanted: return "Encantada";
            default: return "";
        }
    }
}