using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] string game_scene_name = "SampleScene";

    public void Play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(game_scene_name);
    }

    public void OpenSettings()
    {
        Debug.Log("Configurações ainda não implementadas.");
    }

    // Botão "Quit"
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}