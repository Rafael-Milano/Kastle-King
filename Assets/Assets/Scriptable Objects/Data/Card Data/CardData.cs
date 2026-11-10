using UnityEngine;

public enum CardRarity { Common, Rare, Enchanted }
public enum CardStat { Damage, AttackSpeed, Health, Defense, CriticalChance, CriticalDamage, Multishot, Pierce, CritHaste}
public enum CardBonusType { Percent, Flat }

[CreateAssetMenu(fileName = "NewCard", menuName = "Kastle King/Card Data")]
public class CardData : ScriptableObject
{
    public string card_name;
    public CardRarity rarity;
    public CardStat stat;
    public CardBonusType bonus_type;

    [Header("Valor")]
    public float multiplier = 1.1f;   // Percent: 1.1 = +10%
    public float flat_amount = 5f;    // Flat: 5 = +5

    public string Description
    {
        get
        {

            if (stat == CardStat.CritHaste)
            {
                return StatLabel() + " " + (flat_amount * 100f).ToString("0.##") + "% por alguns segundos";
            }

            if (bonus_type == CardBonusType.Percent)
            {
                float percent = (multiplier - 1f) * 100f;
                return StatLabel() + " +" + percent.ToString("0.##") + "%";
            }

            return StatLabel() + " +" + flat_amount.ToString("0.##");
        }
    }

    string StatLabel()
    {
        switch (stat)
        {
            case CardStat.Damage: return "Aumenta o dano causado em";
            case CardStat.AttackSpeed: return "Aumenta a velocidade de ataque";
            case CardStat.Health: return "Aumenta a vida maxima em";
            case CardStat.Defense: return "Aumenta a defesa em";
            case CardStat.CriticalChance: return "Aumenta a chance de causar dano crítico em";
            case CardStat.CriticalDamage: return "Multiplica o dano crítico em";
            case CardStat.Multishot: return "Aumenta a quantidade de projéteis em";
            case CardStat.Pierce: return "Os projéteis atravessam mais inimigos:";
            case CardStat.CritHaste: return "Acertos críticos aumentam a velocidade de ataque em";
            default: return stat.ToString();
        }
    }

    public void Apply(CastleStatus status)
    {
        switch (stat)
        {
            case CardStat.Damage:
                if (bonus_type == CardBonusType.Percent) status.MultiplyDamage(multiplier);
                else status.AddDamage(flat_amount);
                break;
            case CardStat.AttackSpeed:
                if (bonus_type == CardBonusType.Percent) status.MultiplyAttackSpeed(multiplier);
                else status.AddAttackSpeed(flat_amount);
                break;
            case CardStat.Health:
                if (bonus_type == CardBonusType.Percent) status.MultiplyHealth(multiplier);
                else status.AddHealth(flat_amount);
                break;
            case CardStat.Defense:
                if (bonus_type == CardBonusType.Percent) status.MultiplyDefense(multiplier);
                else status.AddDefense(flat_amount);
                break;
            case CardStat.CriticalChance:
                if (bonus_type == CardBonusType.Percent) status.MultiplyCritChance(multiplier);
                else status.AddCritChance(flat_amount);
                break;
            case CardStat.CriticalDamage:
                if (bonus_type == CardBonusType.Percent) status.MultiplyCritDamage(multiplier);
                else status.AddCritDamage(flat_amount);
                break;
            case CardStat.Multishot:
                status.AddProjectileCount(Mathf.RoundToInt(flat_amount));
                break;
            case CardStat.Pierce:
                status.AddPierceCount(Mathf.RoundToInt(flat_amount));
                break;
            case CardStat.CritHaste:
                status.AddCritHaste(flat_amount);
                break;

        }
    }
}