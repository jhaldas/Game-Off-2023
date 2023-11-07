using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponScript : MonoBehaviour
{
    
    private Vector2 playerMousePosition, mousePositionWorld, playerToMouseDirection;
    private bool playerFiring = false;
    private PlayerWeaponDictionary playerWeapons = new PlayerWeaponDictionary();
    private PlayerWeaponDictionary.Weapons playerCurrentWeapon = PlayerWeaponDictionary.Weapons.Pistol;
    private Action pistolFire, swordFire, sMGFire, shotgunFire, rifleFire, grenadeLauncherFire;
    public GameObject playerBulletPrefab, playerGrenadePrefab, playerSlashPrefab, playerShotgunPelletPrefab;
    private Quaternion quaternionPlayerToMouse;
    private float playerToMouseAngle;

    // Weapon constants
    const int weaponValueElementLength          = 6; // Change value when adding another stat to weapon values.
    const int weaponReloadElement               = 0;
    const int weaponDamageElement               = 1;
    const int weaponShotspeedElement            = 2;
    const int weaponKnockbackElement            = 3;
    const int weaponSpreadElement               = 4;
    const int weaponRangeElement                = 5;

    [SerializeField] private float[] weaponReloadTimers = new float[weaponValueElementLength]; // Be careful adding weapon values because the editor might freak out.
    private float[][] weaponValues = new float[Enum.GetValues(typeof(PlayerWeaponDictionary.Weapons)).Length][]; // Cannot be serialized on unity
    [SerializeField] private bool[] weaponHasFired = {false, false, false, false, false, false};

    [SerializeField] private GameObject weaponHolder; // Parent game object for all weapon sprites

    [SerializeField] private GameObject[] weaponSprites = new GameObject[6];

    void Start()
    {
        InitializeWeaponLambdas();
        InitializeWeaponValues();
    }

    void Update()
    {
        MouseToWorldUpdate();
        HandlePlayerShoot();
        WeaponReloadUpdate();
        HandleWeaponRotation();
    }

    /// <summary>
    /// Handles when player fires.
    /// </summary>
    public void OnPlayerFire(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Started || context.action.phase == InputActionPhase.Performed)
        {
            playerFiring = true;
            
        }
        if(context.action.phase == InputActionPhase.Canceled)
        {
            playerFiring = false;
        }
    }

    /// <summary>
    /// Gets the player's current mouse position;
    /// </summary>
    /// <returns>Mouse position in a vector 2.</returns>
    public Vector2 GetMousePosition()
    {
        return playerMousePosition;
    }

    public void OnWeaponSwitch(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Started)
        {
            switch(context.control.name)
            {
                case "1": // Pistol
                    HandleWeaponSwitch(PlayerWeaponDictionary.Weapons.Pistol);
                    break;
                case "2": // Sword
                    HandleWeaponSwitch(PlayerWeaponDictionary.Weapons.Sword);
                    break;
                case "3": // SMG
                    HandleWeaponSwitch(PlayerWeaponDictionary.Weapons.SMG);
                    break;
                case "4": // Shotgun
                    HandleWeaponSwitch(PlayerWeaponDictionary.Weapons.Shotgun);
                    break;
                case "5": // Rifle
                    HandleWeaponSwitch(PlayerWeaponDictionary.Weapons.Rifle);
                    break;
                case "6": // Grenade Launcher
                    HandleWeaponSwitch(PlayerWeaponDictionary.Weapons.GrenadeLauncher);
                    break;
            }
        }
    }

    private void HandleWeaponSwitch(PlayerWeaponDictionary.Weapons weapon)
    {
        if(playerWeapons.GetHasWeapon(weapon))
        {
            if(!(playerCurrentWeapon == weapon))
            {
                HandleWeaponValueUpdate(weapon);
                playerCurrentWeapon = weapon;
                HandleWeaponSpriteSwitch(playerCurrentWeapon);
                Debug.Log("Weapon " + playerCurrentWeapon.ToString() + " Equipped!");
            }
            else
            {
                Debug.Log("Weapon Equipped Already!");
            }
        }
        else
        {
            Debug.Log("Weapon Locked!");
        }
    }

    private void HandlePlayerShoot()
    {
        if(playerFiring)
        {
            if(!weaponHasFired[(int)playerCurrentWeapon])
            {
                HandleWeaponShoot(playerCurrentWeapon);
            }
        }
    }

    private void HandleWeaponSpriteSwitch(PlayerWeaponDictionary.Weapons weapon) /// For when switching weapons.
    {
        foreach (GameObject s in weaponSprites) 
        {
            s.SetActive(false);
        }

        switch (weapon)
        {
            case PlayerWeaponDictionary.Weapons.Pistol: // Pistol
                weaponSprites[0].SetActive(true);
                break;
            case PlayerWeaponDictionary.Weapons.Sword: // Sword
                weaponSprites[1].SetActive(true);
                break;
            case PlayerWeaponDictionary.Weapons.SMG: // SMG
                weaponSprites[2].SetActive(true);
                break;
            case PlayerWeaponDictionary.Weapons.Shotgun: // Shotgun
                weaponSprites[3].SetActive(true);
                break;
            case PlayerWeaponDictionary.Weapons.Rifle: // Rifle
                weaponSprites[4].SetActive(true);
                break;
            case PlayerWeaponDictionary.Weapons.GrenadeLauncher: // Grenade Launcher
                weaponSprites[5].SetActive(true);
                break;
        }
    }

    private void HandleWeaponShoot(PlayerWeaponDictionary.Weapons weapon)
    {
        switch(weapon)
        {
            case PlayerWeaponDictionary.Weapons.Pistol: // Pistol
                if(!weaponHasFired[(int)weapon])
                {
                    weaponHasFired[(int)weapon] = true;
                    FromPlayerToMouseQuaternion();
                    pistolFire.Invoke();
                    HandleWeaponShootingAnimation(weapon);
                    weaponReloadTimers[(int)weapon] = weaponValues[(int)weapon][weaponReloadElement];
                }
                break;
            case PlayerWeaponDictionary.Weapons.Sword: // Sword
                if(!weaponHasFired[(int)weapon])
                {
                    weaponHasFired[(int)weapon] = true;
                    FromPlayerToMouseQuaternion();
                    swordFire.Invoke();
                    HandleWeaponShootingAnimation(weapon);
                    weaponReloadTimers[(int)weapon] = weaponValues[(int)weapon][weaponReloadElement];
                }
                break;
            case PlayerWeaponDictionary.Weapons.SMG: // SMG
                if(!weaponHasFired[(int)weapon])
                {
                    weaponHasFired[(int)weapon] = true;
                    FromPlayerToMouseQuaternion();
                    sMGFire.Invoke();
                    HandleWeaponShootingAnimation(weapon);
                    weaponReloadTimers[(int)weapon] = weaponValues[(int)weapon][weaponReloadElement];
                }
                break;
            case PlayerWeaponDictionary.Weapons.Shotgun: // Shotgun
                if(!weaponHasFired[(int)weapon])
                {
                    weaponHasFired[(int)weapon] = true;
                    FromPlayerToMouseQuaternion();
                    shotgunFire.Invoke();
                    HandleWeaponShootingAnimation(weapon);
                    weaponReloadTimers[(int)weapon] = weaponValues[(int)weapon][weaponReloadElement];
                }
                break;
            case PlayerWeaponDictionary.Weapons.Rifle: // Rifle
                if(!weaponHasFired[(int)weapon])
                {
                    weaponHasFired[(int)weapon] = true;
                    FromPlayerToMouseQuaternion();
                    rifleFire.Invoke();
                    HandleWeaponShootingAnimation(weapon);
                    weaponReloadTimers[(int)weapon] = weaponValues[(int)weapon][weaponReloadElement];
                }
                break;
            case PlayerWeaponDictionary.Weapons.GrenadeLauncher: // Grenade Launcher
                if(!weaponHasFired[(int)weapon])
                {
                    weaponHasFired[(int)weapon] = true;
                    FromPlayerToMouseQuaternion();
                    grenadeLauncherFire.Invoke();
                    HandleWeaponShootingAnimation(weapon);
                    weaponReloadTimers[(int)weapon] = weaponValues[(int)weapon][weaponReloadElement];
                }
                break;
        }
    }

    private void HandleWeaponShootingAnimation(PlayerWeaponDictionary.Weapons weapon) /// For when shooting weapon.
    {
        switch(weapon)
        {
            case PlayerWeaponDictionary.Weapons.Pistol: // Pistol
                break;
            case PlayerWeaponDictionary.Weapons.Sword: // Sword
                break;
            case PlayerWeaponDictionary.Weapons.SMG: // SMG
                break;
            case PlayerWeaponDictionary.Weapons.Shotgun: // Shotgun
                break;
            case PlayerWeaponDictionary.Weapons.Rifle: // Rifle
                break;
            case PlayerWeaponDictionary.Weapons.GrenadeLauncher: // Grenade Launcher
                break;
        }
    }

    private void InitializeWeaponValues()
    {
        foreach(PlayerWeaponDictionary.Weapons weapon in Enum.GetValues(typeof(PlayerWeaponDictionary.Weapons)))
        {
            HandleWeaponValueUpdate(weapon);
        }
    }

    private void InitializeWeaponLambdas() // Need to add weapon noises
    {
        pistolFire = () => // Pistol
        {
            GameObject temp = Instantiate(playerBulletPrefab, transform.position, quaternionPlayerToMouse * Quaternion.Euler(0,0,0 + HandleWeaponSpread(PlayerWeaponDictionary.Weapons.Pistol)));
            PlayerBulletScript tempBulletScript = temp.GetComponent<PlayerBulletScript>();
            tempBulletScript.SetBulletValues(weaponValues[(int)PlayerWeaponDictionary.Weapons.Pistol][weaponRangeElement], weaponValues[(int)PlayerWeaponDictionary.Weapons.Pistol][weaponDamageElement], weaponValues[(int)PlayerWeaponDictionary.Weapons.Pistol][weaponShotspeedElement],weaponValues[(int)PlayerWeaponDictionary.Weapons.Pistol][weaponKnockbackElement]);
        };
        swordFire = () => // Sword (might change how range and offset work)
        {
            GameObject temp = Instantiate(playerSlashPrefab, transform.position + (new Vector3(playerToMouseDirection.x,playerToMouseDirection.y,0) * 3), quaternionPlayerToMouse);
            temp.transform.localScale = new Vector2(weaponValues[(int)PlayerWeaponDictionary.Weapons.Sword][weaponRangeElement]/2,weaponValues[(int)PlayerWeaponDictionary.Weapons.Sword][weaponRangeElement]*2);
        };
        sMGFire = () => // SMG
        {
            GameObject temp = Instantiate(playerBulletPrefab, transform.position, quaternionPlayerToMouse * Quaternion.Euler(0,0,0 + HandleWeaponSpread(PlayerWeaponDictionary.Weapons.SMG)));
            PlayerBulletScript tempBulletScript = temp.GetComponent<PlayerBulletScript>();
            tempBulletScript.SetBulletValues(weaponValues[(int)PlayerWeaponDictionary.Weapons.SMG][weaponRangeElement], weaponValues[(int)PlayerWeaponDictionary.Weapons.SMG][weaponDamageElement], weaponValues[(int)PlayerWeaponDictionary.Weapons.SMG][weaponShotspeedElement],weaponValues[(int)PlayerWeaponDictionary.Weapons.SMG][weaponKnockbackElement]);
        };
        shotgunFire = () => // Shotgun (need to add spread values and potential bullet count)
        {
            GameObject[] temp = new GameObject[9];
            for (int i = 0; i < 9; i++)
            {
                temp[i] = Instantiate(playerShotgunPelletPrefab, transform.position, quaternionPlayerToMouse * Quaternion.Euler(0,0,0 + HandleWeaponSpread(PlayerWeaponDictionary.Weapons.Shotgun)));
                PlayerBulletScript tempBulletScript = temp[i].GetComponent<PlayerBulletScript>();
                tempBulletScript.SetBulletValues(weaponValues[(int)PlayerWeaponDictionary.Weapons.Shotgun][weaponRangeElement], weaponValues[(int)PlayerWeaponDictionary.Weapons.Shotgun][weaponDamageElement], HandleVaryingShotspeed(PlayerWeaponDictionary.Weapons.Shotgun,3),weaponValues[(int)PlayerWeaponDictionary.Weapons.Shotgun][weaponKnockbackElement]);
            }
        };
        rifleFire = () => // Rifle
        {
            GameObject temp = Instantiate(playerBulletPrefab, transform.position, quaternionPlayerToMouse * Quaternion.Euler(0,0,0 + HandleWeaponSpread(PlayerWeaponDictionary.Weapons.Rifle)));
            PlayerBulletScript tempBulletScript = temp.GetComponent<PlayerBulletScript>();
            tempBulletScript.SetBulletValues(weaponValues[(int)PlayerWeaponDictionary.Weapons.Rifle][weaponRangeElement], weaponValues[(int)PlayerWeaponDictionary.Weapons.Rifle][weaponDamageElement], weaponValues[(int)PlayerWeaponDictionary.Weapons.Rifle][weaponShotspeedElement],weaponValues[(int)PlayerWeaponDictionary.Weapons.Rifle][weaponKnockbackElement]);
        };
        grenadeLauncherFire = () => // Grenade Launcher
        {
            Instantiate(playerGrenadePrefab, transform.position, quaternionPlayerToMouse);
        };
    }

    private void FromPlayerToMouseQuaternion()
    {
        quaternionPlayerToMouse = Quaternion.Euler(0, 0, playerToMouseAngle);
    }

    private void MouseToWorldUpdate()
    {
        playerMousePosition = Mouse.current.position.ReadValue();
        mousePositionWorld = Camera.main.ScreenToWorldPoint(playerMousePosition);
        playerToMouseDirection = (mousePositionWorld - new Vector2(transform.position.x,transform.position.y)).normalized;
        playerToMouseAngle = Mathf.Atan2(playerToMouseDirection.y, playerToMouseDirection.x) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// This function will be the main function that will be called when anything is changed.
    /// </summary>
    /// <param name="weapon">Weapon that needs to be updated.</param>
    public void HandleWeaponValueUpdate(PlayerWeaponDictionary.Weapons weapon)
    {
       weaponValues[(int)weapon] = playerWeapons.GetWeaponValues(weapon);
    }

    /// <summary>
    /// Global function to add a float value to a specific weapon value.
    /// </summary>
    /// <param name="weapon">One of the weapons in the dictionary.</param>
    /// <param name="weaponValueElement">0 = Reload Speed | 1 = Damage | 2 = Shotspeed | 3 = Knockback | 4 = Spread | 5 = Range</param>
    /// <param name="value">Float value to add</param>
    public void AddToWeaponValues(PlayerWeaponDictionary.Weapons weapon, int weaponValueElement, float value)
    {
        playerWeapons.AddWeaponValue(weapon, weaponValueElement, value);
    }

    /// <summary>
    /// Global function to multiply a float value to a specific weapon value.
    /// </summary>
    /// <param name="weapon">One of the weapons in the dictionary.</param>
    /// <param name="weaponValueElement">0 = Reload Speed | 1 = Damage | 2 = Shotspeed | 3 = Knockback | 4 = Spread | 5 = Range</param>
    /// <param name="value">Float value to add</param>
    public void MultiplyToWeaponValues(PlayerWeaponDictionary.Weapons weapon, int weaponValueElement, float value)
    {
        playerWeapons.AddWeaponValue(weapon, weaponValueElement, value);
    }

    private void WeaponReloadUpdate()
    {
        for(int i = 0; i < weaponValueElementLength; i++)
        {
            if(weaponReloadTimers[i] > 0)
            {
                weaponReloadTimers[i] -= Time.deltaTime;
            }
            else if(weaponReloadTimers[i] <= 0 && weaponHasFired[i])
            {
                weaponHasFired[i] = false;
            }
        }
    }

    private float HandleWeaponSpread(PlayerWeaponDictionary.Weapons weapon)
    {
        return UnityEngine.Random.Range(-weaponValues[(int)weapon][weaponSpreadElement],weaponValues[(int)weapon][weaponSpreadElement]);
    }

    private float HandleVaryingShotspeed(PlayerWeaponDictionary.Weapons weapon, float varyingAmount)
    {
        return UnityEngine.Random.Range(weaponValues[(int)weapon][weaponShotspeedElement]-varyingAmount,weaponValues[(int)weapon][weaponShotspeedElement]);
    }

    private void HandleWeaponRotation() 
    {
        weaponHolder.transform.rotation = Quaternion.Euler(0, 0, playerToMouseAngle);
    }

    private void ShowCorrectWeapon()
    {

    }
}

/**

**/