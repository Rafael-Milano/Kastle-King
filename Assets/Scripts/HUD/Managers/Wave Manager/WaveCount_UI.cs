using UnityEngine;
using UnityEngine.UI;

public class WaveCount_UI : MonoBehaviour
{
    [SerializeField] WaveManager wave_manager;

    public Text contaddor;

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
        contaddor.text = "Wave " + wave;
    }
}
