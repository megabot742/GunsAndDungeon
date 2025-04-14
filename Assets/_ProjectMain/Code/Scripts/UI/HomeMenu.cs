using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MainLevel");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
