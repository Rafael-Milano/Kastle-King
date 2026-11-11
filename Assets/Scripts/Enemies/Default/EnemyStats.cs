using System;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] EnemyData data;

    float hp;
    bool is_dead = false;
    public event Action OnDeath;

    public static event Action<Vector3, float, bool> OnAnyDamaged; // (position, damage, crit)

    private void Start()
    {
        hp = data.max_hp;
    }

    public void TakeDamage(float damage, bool is_crit = false)
    {
        if (is_dead) return;

        hp -= damage;

        OnAnyDamaged?.Invoke(transform.position, damage, is_crit);

        if (hp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        is_dead = true;
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    // ------------------------------

    public float Damage => data.dmg;
    public float AttackSpeed => data.atk_speed;
    public float MoveSpeed => data.mov_speed;
}