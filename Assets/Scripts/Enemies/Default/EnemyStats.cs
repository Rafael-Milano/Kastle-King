using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] EnemyData data;

    float hp;

    private void Start()
    {
        hp = data.max_hp;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(this.gameObject);
        
    }

    // ------------------------------

    public float Damage => data.dmg;
    public float AttackSpeed => data.atk_speed;
    public float MoveSpeed => data.mov_speed;

}
