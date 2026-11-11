using UnityEngine;

public class DamageNumberSpawner : MonoBehaviour
{
    [SerializeField] DamageNumber damage_number_prefab;
    [SerializeField] Vector3 offset = new Vector3(0f, 0.5f, 0f);
    [SerializeField] float random_x = 0.3f;

    private void OnEnable()
    {
        EnemyStats.OnAnyDamaged += Spawn;
    }

    private void OnDisable()
    {
        EnemyStats.OnAnyDamaged -= Spawn;
    }

    void Spawn(Vector3 position, float damage, bool is_crit)
    {
        float x = Random.Range(-random_x, random_x);
        Vector3 spawn_position = position + offset + new Vector3(x, 0f, 0f);

        DamageNumber number = Instantiate(damage_number_prefab, spawn_position, Quaternion.identity);
        number.Setup(damage, is_crit);
    }
}