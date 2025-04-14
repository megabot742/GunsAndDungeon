using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] ParticleSystem pickupVFX;
    const string PLAYER_STRING = "Player";
    void Update()
    {
        transform.Rotate(0f,rotationSpeed*Time.deltaTime,0f);
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag(PLAYER_STRING))
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
            OnPickup(activeWeapon);
            Instantiate(pickupVFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    protected abstract void OnPickup(ActiveWeapon activeWeapon);
}
