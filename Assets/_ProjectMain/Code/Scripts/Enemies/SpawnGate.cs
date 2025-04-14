using System.Collections;
using StarterAssets;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject robotPrefabs;
    [SerializeField] float spawnTime = 20f;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform spawnParent;
    PlayerHealth playerHealth;
    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(SpawnRobot());
    }
    // Update is called once per frame
    IEnumerator SpawnRobot()
    {
        while(playerHealth)
        {
            yield return new WaitForSeconds(spawnTime);
            Instantiate(robotPrefabs, spawnPoint.position, transform.rotation, spawnParent);
        }
    }
}
