using System;
using UnityEngine;
using UnityEngine.UI;

public class Health_UI : MonoBehaviour
{
    [SerializeField] CastleStatus castle_status;
    [SerializeField] Slider health_bar;

    [SerializeField] Text health_text;

    private void Start()
    {
        health_bar.maxValue = castle_status.MaxHealth;
    }

    private void Update()
    {
        health_bar.value = castle_status.Health;

        int current = Mathf.CeilToInt(castle_status.Health);
        int max = Mathf.CeilToInt(castle_status.MaxHealth);
        health_text.text = current + "/" + max;
    }
}
