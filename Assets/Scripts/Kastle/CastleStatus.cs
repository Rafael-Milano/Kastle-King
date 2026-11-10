using Unity.VisualScripting;
using UnityEngine;

public class CastleStatus : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float health;
    [SerializeField] float max_health = 1000f;

    public float Health => health;
    public float MaxHealth => max_health;

    // ------------------------------------------------

    [Header("Attack")]
    [SerializeField] float damage = 10f;
    [SerializeField] float attack_speed = 2;

    float damage_multiplier = 1f;
    float attack_speed_multiplier = 1f;

    // Flat bonuses (de cartas)
    float damage_flat = 0f;
    float attack_speed_flat = 0f;

    // ------------------------------------------------

    [Header("Defense")]
    [SerializeField] float defense = 0f;

    public float Defense => defense;

    // ------------------------------------------------

    [Header("Critical Damage")]
    [SerializeField] float crit_chance = 0.05f;
    [SerializeField] float crit_multiplier = 2f;

    // ------------------------------------------------

    [Header("Crit Haste")]
    [SerializeField] float crit_haste_duration = 3f;

    float crit_haste_bonus = 0f;
    float crit_haste_end_time = 0f;

    // ------------------------------------------------

    [Header("Projectiles")]
    [SerializeField] int projectile_count = 1;
    [SerializeField] int max_projectile_count = 10;

    // Piercing

    [SerializeField] int pierce_count = 0;
    [SerializeField] int max_pierce_count = 10;

    int pierce_bonus = 0;

    // ------------------------------------------------

    int projectile_bonus = 0;

    private void Start()
    {
        health = max_health;
    }


    public void TakeDamage(float amount)
    {
        health -= amount;
        health = Mathf.Max(health, 0f);
    }

    // 

    public float GetFinalDamage()
    {
        return (damage + damage_flat) * damage_multiplier;
    }

    public float GetFinalAttackSpeed()
    {
        float final_speed = (attack_speed + attack_speed_flat) * attack_speed_multiplier;

        if (IsCritHasteActive)
        {
            final_speed *= 1f + crit_haste_bonus;
        }

        return Mathf.Max(final_speed, 0.5f);
    }

    public float GetCritChance()
    {
        return Mathf.Clamp01(crit_chance);
    }

    public float GetCritMultiplier()
    {
        return Mathf.Max(crit_multiplier, 1f);
    }

    public int GetProjectileCount()
    {
        return Mathf.Clamp(projectile_count + projectile_bonus, 1, max_projectile_count);
    }

    public int GetPierceCount()
    {
        return Mathf.Clamp(pierce_count + pierce_bonus, 0, max_pierce_count);
    }



    // Calcula o dano de UM tiro, já sorteando o crítico
    public float RollDamage(out bool is_crit)
    {
        float final_damage = GetFinalDamage();

        is_crit = UnityEngine.Random.value < GetCritChance();

        if (is_crit)
        {
            final_damage *= GetCritMultiplier();
        }

        return final_damage;
    }


    // Chamados pelas cartas ----------------------

    // Danmage
    public void MultiplyDamage(float factor)
    {
        damage_multiplier *= factor;
    }

    public void AddDamage(float amount)
    {
        damage_flat += amount;
    }

    // Atk speed

    public void MultiplyAttackSpeed(float factor)
    {
        attack_speed_multiplier *= factor;
    }

    public void AddAttackSpeed(float amount)
    {
        attack_speed_flat += amount;
    }

    // Health

    public void MultiplyHealth(float factor)
    {
        max_health *= factor;
        health *= factor;
    }
    public void AddHealth(float factor)
    {
        max_health += factor;
        health += factor;
    }

    // Defense

    public void MultiplyDefense(float factor)
    {
        defense *= factor;
    }
    public void AddDefense(float factor)
    {
        defense += factor;
    }

    // Crit
    public void MultiplyCritChance(float factor)
    {
        crit_chance *= factor;
    }

    public void AddCritChance(float factor)
    {
        crit_chance += factor;
    }

    public void MultiplyCritDamage(float factor)
    {
        crit_multiplier *= factor;
    }

    public void AddCritDamage(float factor)
    {
        crit_multiplier += factor;
    }

    // Multishot

    public void AddProjectileCount(int amount)
    {
        projectile_bonus += amount;
    }

    // Piercing

    public void AddPierceCount(int amount)
    {
        pierce_bonus += amount;
    }

    // Crit Haste

    public bool IsCritHasteActive => Time.time < crit_haste_end_time;

    public void AddCritHaste(float amount)
    {
        crit_haste_bonus += amount;
    }

    public void OnCritHit()
    {
        if (crit_haste_bonus <= 0f) return;   // sem a carta, não faz nada

        crit_haste_end_time = Time.time + crit_haste_duration;
    }
    // --------------------------------------------------
}
