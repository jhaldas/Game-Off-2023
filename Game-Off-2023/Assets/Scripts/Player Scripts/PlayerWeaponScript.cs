using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponScript : MonoBehaviour
{
    
    private Vector2 playerMousePosition, mousePositionWorld, playerToMouseDirection;
    private bool playerFiring = false;
    private bool playerHasFired = false;
    private PlayerWeaponDictionary playerWeapons = new PlayerWeaponDictionary();
    private PlayerWeaponDictionary.Weapons playerCurrentWeapon = PlayerWeaponDictionary.Weapons.Pistol;
    private Action pistolFire, swordFire, sMGFire, shotgunFire, rifleFire, grenadeLauncherFire;
    public GameObject playerBulletPrefab, playerGrenadePrefab;
    private Quaternion quaternionPlayerToMouse;
    private float playerToMouseAngle, pistolReloadSpeed, swordReloadSpeed, sMGReloadSpeed, shotgunReloadSpeed, rifleReloadSpeed, grenadeLauncherReloadSpeed;

    void Start()
    {
        InitializeWeaponLambdas();
    }

    void Update()
    {
        HandleMouseToWorld();
        HandlePlayerShoot();
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
            if(!playerHasFired)
            {
                HandleWeaponShoot(playerCurrentWeapon);
            }
        }
    }

    private void HandleWeaponSpriteSwitch(PlayerWeaponDictionary.Weapons weapon) /// For when switching weapons.
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

    private void HandleWeaponShoot(PlayerWeaponDictionary.Weapons weapon)
    {
        switch(weapon)
        {
            case PlayerWeaponDictionary.Weapons.Pistol: // Pistol
                FromPlayerToMouseQuaternion();
                pistolFire.Invoke();
                HandleWeaponShootingAnimation(weapon);
                break;
            case PlayerWeaponDictionary.Weapons.Sword: // Sword
                FromPlayerToMouseQuaternion();
                swordFire.Invoke();
                HandleWeaponShootingAnimation(weapon);
                break;
            case PlayerWeaponDictionary.Weapons.SMG: // SMG
                FromPlayerToMouseQuaternion();
                sMGFire.Invoke();
                HandleWeaponShootingAnimation(weapon);
                break;
            case PlayerWeaponDictionary.Weapons.Shotgun: // Shotgun
                FromPlayerToMouseQuaternion();
                shotgunFire.Invoke();
                HandleWeaponShootingAnimation(weapon);
                break;
            case PlayerWeaponDictionary.Weapons.Rifle: // Rifle
                FromPlayerToMouseQuaternion();
                rifleFire.Invoke();
                HandleWeaponShootingAnimation(weapon);
                break;
            case PlayerWeaponDictionary.Weapons.GrenadeLauncher: // Grenade Launcher
                FromPlayerToMouseQuaternion();
                grenadeLauncherFire.Invoke();
                HandleWeaponShootingAnimation(weapon);
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

    private void InitializeWeaponLambdas()
    {
        pistolFire = () =>
        {
            Instantiate(playerBulletPrefab, transform.position, quaternionPlayerToMouse);
        };
        swordFire = () =>
        {
            Instantiate(playerBulletPrefab, transform.position, Quaternion.Euler(0,0,0));
        };
        sMGFire = () =>
        {
            Instantiate(playerBulletPrefab, transform.position, Quaternion.Euler(0,0,0));
        };
        shotgunFire = () =>
        {
            Instantiate(playerBulletPrefab, transform.position, Quaternion.Euler(0,0,0));
        };
        rifleFire = () =>
        {
            Instantiate(playerBulletPrefab, transform.position, Quaternion.Euler(0,0,0));
        };
        grenadeLauncherFire = () =>
        {
            Instantiate(playerBulletPrefab, transform.position, Quaternion.Euler(0,0,0));
        };
    }

    private void FromPlayerToMouseQuaternion()
    {
        quaternionPlayerToMouse = Quaternion.Euler(0, 0, playerToMouseAngle);
    }

    private void HandleMouseToWorld()
    {
        playerMousePosition = Mouse.current.position.ReadValue();
        mousePositionWorld = Camera.main.ScreenToWorldPoint(playerMousePosition);
        playerToMouseDirection = (mousePositionWorld - new Vector2(transform.position.x,transform.position.y)).normalized;
        playerToMouseAngle = Mathf.Atan2(playerToMouseDirection.y, playerToMouseDirection.x) * Mathf.Rad2Deg;
    }

    
}

/**

**/