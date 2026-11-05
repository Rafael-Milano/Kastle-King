using UnityEngine;

public class CastleProjectile : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float lifetime = 3f;

    float damage;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
            Destroy(gameObject);
        }     
    }
}