using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 2f;
    [SerializeField] int explodeDmage = 3;
    const string PLAYER_STRING = "Player";
    void Start()
    {
        Explode();
    }
    void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider item in hitColliders)
        {  
            PlayerHealth playerHealth = item.GetComponent<PlayerHealth>();
            if(!playerHealth) continue;
            playerHealth.TakeDamage(explodeDmage);
            break;

            //playerHealth?.TakeDamage(explodeDmage);

            // if(playerHealth)
            // {
            //     playerHealth.TakeDamage(explodeDmage);
            // }
        }
    }
}
