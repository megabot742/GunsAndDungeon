using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject player;
    StarterAssetsInputs starterAssetsInputs;
    bool GameIsPaused = false;
    void Start()
    {
        starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) // true
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        starterAssetsInputs.SetCursorState(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        player.SetActive(true);
        GameIsPaused = false;
    }
    public void Pause()
    {
        starterAssetsInputs.SetCursorState(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        player.SetActive(false);
        GameIsPaused = true;

    }
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
