using System;
using UnityEngine;
using UnityEngine.UI;

public class Health_UI : MonoBehaviour
{
    [SerializeField] CastleStatus castle_status;
    [SerializeField] Slider health_bar;

    private void Start()
    {
        health_bar.maxValue = castle_status.MaxHP;
    }

    private void Update()
    {
        health_bar.value = castle_status.HP;
    }
}
