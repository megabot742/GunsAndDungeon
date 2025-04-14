using Unity.VisualScripting;
using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] AmmoType ammoType;
    [SerializeField] int ammoAmount = 10;
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        FindFirstObjectByType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
    }
}
