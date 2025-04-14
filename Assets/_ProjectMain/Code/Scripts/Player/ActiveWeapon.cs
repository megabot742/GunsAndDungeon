using System.Collections;
using UnityEngine;
using StarterAssets;
using Cinemachine;
using TMPro;
using System.Collections.Generic;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeaponSO;
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] Camera weaponCamera;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] TMP_Text ammoText;

    [Header("Gun Status")]
    [SerializeField] WeaponSO currentWeaponSO;
    [SerializeField] Weapon currentWeapon;
    [SerializeField] int currentWeaponIndex = 1;
    [SerializeField] List<Weapon> weaponList = new List<Weapon>();
    [SerializeField] List<WeaponSO> weaponSOList = new List<WeaponSO>();
    [SerializeField] Ammo ammo;

    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;
    const string SHOOT_STRING = "Shoot";
    float timeCoolDownShoot = 0;
    float defaultFOV;
    float defaultRotationSpeed;
    int currentAmmo;

    void Awake() 
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }
    void Start() 
    {
        UnlockWeapon(startingWeaponSO);
    }
    
    void Update()
    {  
        ChangeWeapon();
        HandleShoot();
        HandleZoom();
        DisplayAmmo();
    }
    void DisplayAmmo()
    {
        currentAmmo = ammo.GetCurrentAmmo(currentWeaponSO.ammoType);
        ammoText.text = currentAmmo.ToString("D2");
    }
    public void UnlockWeapon(WeaponSO weaponSO)
    {
        if(weaponList.Count != currentWeaponIndex)
        {
            currentWeaponIndex = weaponList.Count;
        }
        currentWeaponIndex++; //new weapon Unlock
        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon; //change weapon 
        weaponList.Add(currentWeapon); //add to list

        this.currentWeaponSO = weaponSO; //change weaponSO 
        weaponSOList.Add(currentWeaponSO); //add to list
        
        SetWeaponActive(); //Disable another gun
    }
    void SetWeaponActive()
    {
        int weaponIndex = 1;
        foreach (Transform weapon in this.transform)
        {
            if(weaponIndex == currentWeaponIndex)
            {
                weapon.gameObject.SetActive(true);
                currentWeapon = weaponList[currentWeaponIndex - 1];
                currentWeaponSO = weaponSOList[currentWeaponIndex - 1];
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++; //ready for next weapon
        }
    }
    void ChangeWeapon()
    {
        int previousWeapon = currentWeaponIndex;
        ProcessKeyInput(); //keyborad
        ProcessScrollWheel(); //scrollwheel
        if(previousWeapon != currentWeaponIndex)
        {
            SetWeaponActive();
        }
    }
    void ProcessKeyInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeaponIndex = 1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && weaponList.Count>1)
        {
            currentWeaponIndex = 2;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3) && weaponList.Count>2)
        {
            currentWeaponIndex = 3;
        }    
    }
    void ProcessScrollWheel()
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(currentWeaponIndex >= transform.childCount)
            {
                currentWeaponIndex = 1;
            }
            else
            {
                currentWeaponIndex++;
            }
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(currentWeaponIndex <= 1)
            {
                currentWeaponIndex = transform.childCount;
            }
            else
            {
                currentWeaponIndex--;
            }
        }
    }
    void HandleShoot()
    {
        timeCoolDownShoot += Time.deltaTime;
        if(!starterAssetsInputs.shoot) {return;}
        if(timeCoolDownShoot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            currentWeapon.Shoot(currentWeaponSO);
            animator.Play(SHOOT_STRING, 0, 0f); //Animation gun
            timeCoolDownShoot = 0;
        }
        if(!currentWeaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false); //reload status 
        }
    }
    void HandleZoom()
    {
        if(starterAssetsInputs.zoom && currentWeaponSO.CanZoom) // check type of Weapon
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }
    void ZoomIn()
    {
        playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
        weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
        zoomVignette.SetActive(true);
        firstPersonController.SetRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
    }
    void ZoomOut()
    {
        playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
        weaponCamera.fieldOfView = defaultFOV;
        zoomVignette.SetActive(false);
        firstPersonController.SetRotationSpeed(defaultRotationSpeed);
    }  
}
