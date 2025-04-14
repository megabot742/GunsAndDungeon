using StarterAssets;
using TMPro;
//using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject crossHair;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject player;
    public void EnableCrossHair(bool status)
    {
        crossHair.SetActive(status);
    }
    public void RestartLevelButton()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
