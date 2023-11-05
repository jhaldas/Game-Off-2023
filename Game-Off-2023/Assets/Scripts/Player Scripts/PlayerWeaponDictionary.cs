using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponDictionary
{
    
    public enum Weapons 
    {
        Pistol              = 0, 
        Sword               = 1, 
        SMG                 = 2, 
        Shotgun             = 3, 
        Rifle               = 4, 
        GrenadeLauncher     = 5
    };

    private Dictionary<Weapons, float[]> weaponDictionary = new Dictionary<Weapons, float[]>();
    private Dictionary<Weapons, bool> hasWeaponDictionary = new Dictionary<Weapons, bool>();

    const int weaponValueElementLength = 6; // Change value when adding another stat to weapon values.
    // Weapon values will go with the following order reload speed, damage, shotspeed, knockback, spread, range.
    //                                  Reload Speed(Sec)   Damage          Shotspeed       Knockback       Spread      Range(Sec)
    private float[] pistolValues =      {1                  ,2              ,3              ,4              ,1          ,3              };
    private float[] swordValues =       {1                  ,2              ,3              ,4              ,1          ,3              }; // Sword range is the distance from player.
    private float[] sMGValues =         {.3f                ,45             ,1              ,4              ,1          ,3              };
    private float[] shotgunValues =     {1                  ,2              ,10             ,4              ,25         ,3              };
    private float[] rifleValues =       {1                  ,2              ,3              ,4              ,1          ,3              };
    private float[] grenadeValues =     {1                  ,2              ,3              ,4              ,1          ,3              };

    public PlayerWeaponDictionary()
    {
        // Weapon Value Dictionary
        weaponDictionary.Add(Weapons.Pistol, pistolValues);
        weaponDictionary.Add(Weapons.Sword, swordValues);
        weaponDictionary.Add(Weapons.SMG, sMGValues);
        weaponDictionary.Add(Weapons.Shotgun, shotgunValues);
        weaponDictionary.Add(Weapons.Rifle, rifleValues);
        weaponDictionary.Add(Weapons.GrenadeLauncher, grenadeValues);

        // Has Weapon Dictionary
        hasWeaponDictionary.Add(Weapons.Pistol, true);
        hasWeaponDictionary.Add(Weapons.Sword, true);
        hasWeaponDictionary.Add(Weapons.SMG, true);
        hasWeaponDictionary.Add(Weapons.Shotgun, true);
        hasWeaponDictionary.Add(Weapons.Rifle, true);
        hasWeaponDictionary.Add(Weapons.GrenadeLauncher, true);
    }

    /// <summary>
    /// Gets the weapon stats for the player.
    /// </summary>
    /// <param name="weapon">Weapon values to pull from</param>
    /// <returns>An array of weapon stats.</returns>
    /// <exception cref="KeyNotFoundException">Cannot find weapon.</exception>
    public float[] GetWeaponValues(Weapons weapon)
    {
        if (weaponDictionary.TryGetValue(weapon, out float[] value))
        {
            return value;
        }
        else
        {
            throw new KeyNotFoundException("Weapon not found: " + weapon);
        }
    }

    /// <summary>
    /// Sees if the player has the weapon.
    /// </summary>
    /// <param name="weapon">What weapon to check for</param>
    /// <returns>A bool for if the player has the weapon or not</returns>
    /// <exception cref="KeyNotFoundException">Cannot find weapon.</exception>
    public bool GetHasWeapon(Weapons weapon)
    {
        if (hasWeaponDictionary.TryGetValue(weapon, out bool value))
        {
            return value;
        }
        else
        {
            throw new KeyNotFoundException("Weapon not found: " + weapon);
        }
    }

    /// <summary>
    /// Add a float value to the weapon element.
    /// </summary>
    /// <param name="weapon">One of the weapons in the dictionary.</param>
    /// <param name="weaponValueElement">0 = Reload Speed | 1 = Damage | 2 = Shotspeed | 3 = Knockback | 4 = Spread | 5 = Range</param>
    /// <param name="value">Float value to add</param>
    public void AddWeaponValue(Weapons weapon, int weaponValueElement, float value)
    {
        if(weaponValueElement >= weaponValueElementLength)
        {
            Debug.Log("Incorrect Weapon Element Value.");
        }
        else
        {
            float[] tempWeaponValue;
            tempWeaponValue = weaponDictionary[weapon];
            tempWeaponValue[weaponValueElement] = tempWeaponValue[weaponValueElement] + value;
            weaponDictionary[weapon] = tempWeaponValue;
        }
    }

    /// <summary>
    /// Multiply a float value to the weapon element.
    /// </summary>
    /// <param name="weapon">One of the weapons in the dictionary.</param>
    /// <param name="weaponValueElement">0 = Reload Speed | 1 = Damage | 2 = Shotspeed | 3 = Knockback | 4 = Spread | 5 = Range</param>
    /// <param name="value">Float value to multiply</param>
    public void MultipleWeaponValue(Weapons weapon, int weaponValueElement, float value)
    {
        if(weaponValueElement >= weaponValueElementLength)
        {
            Debug.Log("Incorrect Weapon Element Value.");
        }
        else
        {
            float[] tempWeaponValue;
            tempWeaponValue = weaponDictionary[weapon];
            tempWeaponValue[weaponValueElement] = tempWeaponValue[weaponValueElement] * value;
            weaponDictionary[weapon] = tempWeaponValue;
        }
    }

}
