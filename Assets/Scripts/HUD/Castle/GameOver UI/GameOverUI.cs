using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] CastleStatus castle_status;
    [SerializeField] GameObject panel;

    [Header("Menu")]
    [SerializeField] string menu_scene_name = "";   // preencher quando o menu existir

    private void Awake()
    {
        panel.SetActive(false);
    }

    private void OnEnable()
    {
        castle_status.OnDied += Show;
    }

    private void OnDisable()
    {
        castle_status.OnDied -= Show;
    }

    void Show()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    // Botão "Recomeçar"
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Botão "Voltar ao menu"
    public void BackToMenu()
    {
        if (string.IsNullOrEmpty(menu_scene_name))
        {
            Debug.LogWarning("Menu ainda não existe: preencha 'Menu Scene Name' no GameOverUI.");
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(menu_scene_name);
    }
}