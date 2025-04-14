using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField] WeaponSO weaponSO;
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.UnlockWeapon(weaponSO);
    }
}
