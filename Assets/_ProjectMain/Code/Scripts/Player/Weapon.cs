using Cinemachine;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //[SerializeField] float maxDistance = 20f;
    
    [SerializeField] ParticleSystem muzzleFlashFX;
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] AudioSource gunSFX;
    [SerializeField] AmmoType ammoType;
    [SerializeField] Ammo ammo;
    CinemachineImpulseSource impulseSource;
    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        ammo = FindFirstObjectByType<Ammo>();
    }
    public void Shoot(WeaponSO weaponSO)
    {
        if (ammo.GetCurrentAmmo(ammoType) > 0)
        {
            muzzleFlashFX.Play(); //VFX gun
            gunSFX.Play(); //SFX gun
            impulseSource.GenerateImpulse();
            ammo.ReduceCurrentAmmo(ammoType);
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out hit, Mathf.Infinity, interactionLayer, QueryTriggerInteraction.Ignore))
            {
                Instantiate(weaponSO.HitFXPrefabs, hit.point, Quaternion.identity);
                EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();
                enemyHealth?.TakeDamage(weaponSO.Damage);
            }
        }
    }
}
