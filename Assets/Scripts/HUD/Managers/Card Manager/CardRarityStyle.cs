using UnityEngine;

[CreateAssetMenu(fileName = "CardRarityStyle", menuName = "Kastle King/Card Rarity Style")]
public class CardRarityStyle : ScriptableObject
{
    public Sprite common_sprite;
    public Sprite rare_sprite;
    public Sprite enchanted_sprite;

    public Sprite GetSprite(CardRarity rarity)
    {
        switch (rarity)
        {
            case CardRarity.Common: return common_sprite;
            case CardRarity.Rare: return rare_sprite;
            case CardRarity.Enchanted: return enchanted_sprite;
            default: return common_sprite;
        }
    }
}