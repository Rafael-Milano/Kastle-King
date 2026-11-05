using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform target;
    EnemyStats stats;
    Rigidbody2D rb;
    Vector2 move_dir;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<EnemyStats>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            move_dir = direction;
        }
    }

    private void FixedUpdate()  
    {
        rb.linearVelocity = new Vector2(move_dir.x, move_dir.y) * stats.MoveSpeed;
    }
}
