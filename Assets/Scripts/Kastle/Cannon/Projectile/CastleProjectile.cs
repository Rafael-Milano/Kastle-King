using System.Collections.Generic;
using UnityEngine;

public class CastleProjectile : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float lifetime = 3f;

    float damage;
    int pierce_left;
    bool is_done = false;

    bool is_crit;
    CastleStatus owner;

    HashSet<EnemyStats> already_hit = new HashSet<EnemyStats>(); // Evita acertar o mesmo inimigo multiplas vezes

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void SetDamage(float value)
    {
        damage = value;
    }

    public void SetPierce(int value)
    {
        pierce_left = value;
    }

    public void SetCrit(bool crit, CastleStatus status)
    {
        is_crit = crit;
        owner = status;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (is_done) return;

        EnemyStats enemy = other.GetComponentInParent<EnemyStats>();
        if (enemy == null) return;

        if (already_hit.Contains(enemy)) return;
        already_hit.Add(enemy);

        enemy.TakeDamage(damage, is_crit);

        if (is_crit && owner != null)
        {
            owner.OnCritHit();
        }

        if (pierce_left <= 0)
        {
            is_done = true;
            Destroy(gameObject);
        }
        else
        {
            pierce_left--;
        }
    }
}