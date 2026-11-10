using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] Transform spawn_point;
    [SerializeField] float spawn_width = 10f;
    [SerializeField] float time_between_spawns = 0.5f;

    [Header("Enemy Budget")]
    [SerializeField] EnemyData[] enemy_types;     // todas as fichas possíveis
    [SerializeField] int base_budget = 5;         // orçamento da onda 1
    [SerializeField] int budget_per_wave = 3;     // quanto o orçamento cresce por onda

    [Header("Waves")]
    [SerializeField] float time_between_waves = 3f;

    int current_wave = 0;
    int enemies_alive = 0;

    // UI
    public int CurrentWave => current_wave;
    public event Action<int> OnWaveChanged;
    public event Action<int> OnWaveEnded;

    private void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(time_between_waves);

        current_wave++;
        OnWaveChanged?.Invoke(current_wave);

        List<EnemyData> wave_enemies = BuildWave(current_wave);
        enemies_alive = wave_enemies.Count;

        foreach (EnemyData data in wave_enemies)
        {
            SpawnEnemy(data);
            yield return new WaitForSeconds(time_between_spawns);
        }
    }

    // Monta a lista de inimigos da onda "comprando" até acabar o orçamento
    List<EnemyData> BuildWave(int wave)
    {
        int budget = base_budget + budget_per_wave * (wave - 1);

        // 1. Quais tipos já estão liberados nesta onda
        List<EnemyData> unlocked = new List<EnemyData>();
        foreach (EnemyData type in enemy_types)
        {
            if (type.min_wave <= wave && type.cost > 0)
            {
                unlocked.Add(type);
            }
        }

        // 2. Compra inimigos até não conseguir pagar mais nenhum
        List<EnemyData> result = new List<EnemyData>();

        while (true)
        {
            List<EnemyData> affordable = new List<EnemyData>();
            foreach (EnemyData type in unlocked)
            {
                if (type.cost <= budget)
                {
                    affordable.Add(type);
                }
            }

            if (affordable.Count == 0) break;

            EnemyData chosen = affordable[UnityEngine.Random.Range(0, affordable.Count)];
            result.Add(chosen);
            budget -= chosen.cost;
        }

        // 3. Segurança: nunca deixar uma onda vazia (ela nunca terminaria)
        if (result.Count == 0 && unlocked.Count > 0)
        {
            result.Add(unlocked[0]);
        }

        return result;
    }

    void SpawnEnemy(EnemyData data)
    {
        float random_x = UnityEngine.Random.Range(-spawn_width / 2f, spawn_width / 2f);
        Vector3 position = spawn_point.position + new Vector3(random_x, 0f, 0f);

        EnemyStats enemy = Instantiate(data.prefab, position, Quaternion.identity);
        enemy.OnDeath += HandleEnemyDeath;
    }

    void HandleEnemyDeath()
    {
        enemies_alive--;

        if (enemies_alive <= 0)
        {
            EndWave();
        }
    }

    void EndWave()
    {
        OnWaveEnded?.Invoke(current_wave);
    }

    public void BeginNextWave()
    {
        StartCoroutine(StartNextWave());
    }

    private void OnDrawGizmosSelected()
    {
        if (spawn_point == null) return;

        Gizmos.color = Color.red;
        Vector3 left = spawn_point.position + Vector3.left * (spawn_width / 2f);
        Vector3 right = spawn_point.position + Vector3.right * (spawn_width / 2f);
        Gizmos.DrawLine(left, right);
    }
}