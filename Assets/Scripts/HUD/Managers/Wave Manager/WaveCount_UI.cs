using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveCount_UI : MonoBehaviour
{
    [SerializeField] WaveManager wave_manager;

    public TMP_Text contaddor;

    private void OnEnable()
    {
        wave_manager.OnWaveChanged += UpdateText;
    }

    private void OnDisable()
    {
        wave_manager.OnWaveChanged -= UpdateText;
    }

    void UpdateText(int wave)
    {
        contaddor.text = "Onda " + wave;
    }
}
