using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Kastle King/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Status")]
    public float max_hp = 50f;
    public float dmg = 5f;
    public float atk_speed = 1f;
    public float mov_speed = 2f;
}