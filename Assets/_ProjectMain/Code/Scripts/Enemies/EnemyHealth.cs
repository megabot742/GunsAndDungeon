using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject robotExplosionVFX;
    [SerializeField] int startingHealth = 3;
    int currentHealth;

    void Awake()
    {
        currentHealth = startingHealth;
    }
    public void TakeDamage(int hit)
    {
        currentHealth -= hit;
        if(currentHealth <= 0)
        {
            SelfDestruct();
        }
    }
    public void SelfDestruct()
    {
        Instantiate(robotExplosionVFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
