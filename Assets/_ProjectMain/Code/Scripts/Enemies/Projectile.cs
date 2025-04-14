using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] GameObject projectileHitVFX;
    int damage; 
    Rigidbody rb;
    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start() 
    {
        rb.linearVelocity = transform.forward * speed;
    }
    public void Init(int damageProjectile)
    {
        this.damage = damageProjectile;
    }
    private void OnTriggerEnter(Collider other) 
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        playerHealth?.TakeDamage(damage);
        Instantiate(projectileHitVFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
