using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    EnemyStats stats;
    float next_attack_time = 0f;

    private void Awake()
    {
        stats = GetComponent<EnemyStats>();
    }

    // Collider da torre sólido (Is Trigger desmarcado)
    private void OnCollisionStay2D(Collision2D collision)
    {
        TryAttack(collision.gameObject);
    }

    // Collider da torre como trigger (Is Trigger marcado)
    private void OnTriggerStay2D(Collider2D other)
    {
        TryAttack(other.gameObject);
    }

    void TryAttack(GameObject target)
    {
        if (Time.time < next_attack_time) return;

        CastleStatus castle = target.GetComponentInParent<CastleStatus>();
        if (castle == null) return;

        float final_damage = Mathf.Max(stats.Damage - castle.Defense, 1f);
        castle.TakeDamage(final_damage);

        float interval = 1f / stats.AttackSpeed;
        next_attack_time = Time.time + interval;
    }
}