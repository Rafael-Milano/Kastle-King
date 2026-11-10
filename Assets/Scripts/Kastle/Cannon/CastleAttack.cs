using System;
using UnityEngine;

public class CastleAttack : MonoBehaviour
{

    [Header("Castle Status")]
    [SerializeField] CastleStatus castle_status;

    //----------------------------------------------

    [Header("Cannon Shoot")]
    [SerializeField] CastleProjectile projectile_prefab;
    [SerializeField] Transform fire_point;

    float next_fire_time = 0f;

    [SerializeField] float spread_angle = 10f;

    //----------------------------------------------

    [Header("Cannon Aim")]
    [SerializeField] Transform king_pos;
    [SerializeField] float radius = 2f;
    [SerializeField] float angle_offset = 0f;
    Camera cam;

    //----------------------------------------------




    private void Start()
    {
        // CanonAim
        cam = Camera.main;
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;

        CannonAim();

        // Shoot
        if (Input.GetMouseButton(0) && Time.time >= next_fire_time)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            castle_status.MultiplyAttackSpeed(1.2f);
            Debug.Log("Attack speed: " + castle_status.GetFinalAttackSpeed());
        }
    }

    private void CannonAim()
    {
        // 1. Posição do mouse no mundo
        Vector3 mouse_world = cam.ScreenToWorldPoint(Input.mousePosition);
        mouse_world.z = 0f;

        // 2. Direção do rei até o mouse (tamanho 1)
        Vector2 direction = (mouse_world - king_pos.position).normalized;

        // 3. Posiciona o canhão no círculo ao redor do rei
        Vector3 new_position = king_pos.position + (Vector3)(direction * radius);
        new_position.z = transform.position.z;
        transform.position = new_position;

        // 4. Rotaciona o canhão para apontar na direção do mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + angle_offset);
    }

    void Shoot()
    {
        int count = castle_status.GetProjectileCount();

        for (int i = 0; i < count; i++)
        {
            float offset = (i - (count - 1) / 2f) * spread_angle;
            Quaternion rotation = transform.rotation * Quaternion.Euler(0f, 0f, offset);

            CastleProjectile projectile = Instantiate(projectile_prefab, fire_point.position, rotation);

            float damage = castle_status.RollDamage(out bool is_crit);
            projectile.SetDamage(damage);
            projectile.SetPierce(castle_status.GetPierceCount());
            projectile.SetCrit(is_crit, castle_status);


            if (is_crit)
            {
                Debug.Log("CRÍTICO! Dano: " + damage);
            }
        }
        float attack_speed = castle_status.GetFinalAttackSpeed();
        float interval = 1f / attack_speed;
        next_fire_time = Time.time + interval;
    }

}