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

    // ------------------------------------------------

    private void Start()
    {
        health = max_health;
    }


    public void TakeDamage(float amount)
    {
        health -= amount;
        health = Mathf.Max(health, 0f);
    }



    public float GetFinalDamage()
    {
        return damage * damage_multiplier;
    }

    public float GetFinalAttackSpeed()
    {
        float final_speed = attack_speed * attack_speed_multiplier;
        return Mathf.Max(final_speed, 0.1f);
    }



    // Chamados pelas cartas (ainda não usados)
    public void MultiplyDamage(float factor)
    {
        damage_multiplier *= factor;
    }

    public void MultiplyAttackSpeed(float factor)
    {
        attack_speed_multiplier *= factor;
    }

    // --------------------------------------------------

    public float HP => health;
    public float MaxHP => max_health;
}
