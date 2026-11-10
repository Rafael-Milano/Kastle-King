using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] Text name_text;
    [SerializeField] Text description_text;
    [SerializeField] Text rarity_text;
    [SerializeField] Image background;
    [SerializeField] Button button;

    public void Setup(CardData card, System.Action<CardData> on_chosen)
    {
        name_text.text = card.card_name;
        description_text.text = card.Description;
        rarity_text.text = RarityLabel(card.rarity);
        background.color = RarityColor(card.rarity);

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

    Color RarityColor(CardRarity rarity)
    {
        switch (rarity)
        {
            case CardRarity.Common: return new Color(0.75f, 0.75f, 0.75f);
            case CardRarity.Rare: return new Color(0.4f, 0.6f, 1f);
            case CardRarity.Enchanted: return new Color(0.7f, 0.4f, 1f);
            default: return Color.white;
        }
    }
}