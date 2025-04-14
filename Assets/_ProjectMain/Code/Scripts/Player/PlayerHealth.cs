using System;
using System.Linq;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Range(1, 9)]
    [SerializeField] int startingHealth = 9;
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] Image[] shieldBars;
    [SerializeField] GameObject gameOverContainer;
    [SerializeField] AudioSource playerSFX;
    [Header("Winner")]
    [SerializeField] GameObject winContainer;
    [SerializeField] GameObject player;
    int currentHealth;
    int gameOverVirtualCameraPriortiy = 20;
    StarterAssetsInputs starterAssetsInputs;
    GameManager gameManager;
    void Awake()
    {
        currentHealth = startingHealth;
        starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        AdjustShieldUI();
    }
    void Start() 
    {
        starterAssetsInputs.SetCursorState(true);
        gameManager = FindFirstObjectByType<GameManager>();
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public void TakeDamage(int hit)
    {
        currentHealth -= hit;
        playerSFX.Play();
        AdjustShieldUI();
        gameManager.GetComponent<DisplayDamage>().ShowDamageImpact();
        if(currentHealth <= 0)
        {
            GameOverHandler();
        }
    }
    void GameOverHandler()
    {
        weaponCamera.parent = null;
        deathVirtualCamera.Priority = gameOverVirtualCameraPriortiy;
        gameOverContainer.SetActive(true);
        starterAssetsInputs.SetCursorState(false);
        gameManager.EnableCrossHair(false);
        Destroy(this.gameObject);
    }
    public void WinHandler()
    {
        winContainer.SetActive(true);
        starterAssetsInputs.SetCursorState(false);
        gameManager.EnableCrossHair(false);
        player.SetActive(false);
    }
    void AdjustShieldUI()
    {
        for(int i=0; i < shieldBars.Length; i++)
        {
            if(i < currentHealth)
            {
                shieldBars[i].gameObject.SetActive(true);
            }
            else
            {
                shieldBars[i].gameObject.SetActive(false);
            }
        }
        
    }
}
