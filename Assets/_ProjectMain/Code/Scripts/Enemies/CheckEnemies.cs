using TMPro;
using UnityEngine;

public class CheckEnemies : MonoBehaviour
{
    [SerializeField] TMP_Text enemiesLeftText;
    [SerializeField] Transform parentObject;
    [SerializeField] int enemiesLeft = 0;
    const string ENEMIES_LEFT_STRING = "Enemies Left: ";
    PlayerHealth playerHealth;
    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }
    void Update()
    {
        AdjustEnemiesLeft();
    }
    void AdjustEnemiesLeft()
    {
        enemiesLeft = parentObject.childCount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();

        if(enemiesLeft <= 0 && playerHealth.GetCurrentHealth() > 0)
        {
            playerHealth.WinHandler();
        }
    }
}
