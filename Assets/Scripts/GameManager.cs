using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject startMenu;
    public GameObject gameOverPanel;

    public MonoBehaviour playerController;
    public SpawnManager spawnManager;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 0f;

        startMenu.SetActive(true);
        gameOverPanel.SetActive(false);

        playerController.enabled = false;
        spawnManager.enabled = false;
    }

    public void StartGame()
    {
        startMenu.SetActive(false);

        GameState.isGameOver = false;
        GameState.isGameStarted = true;

        playerController.enabled = true;
        spawnManager.enabled = true;

        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        GameState.isGameOver = true;

        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}