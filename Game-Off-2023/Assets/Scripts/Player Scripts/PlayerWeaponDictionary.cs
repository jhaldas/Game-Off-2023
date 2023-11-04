using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponDictionary
{
    
    public enum Weapons {Pistol, Sword, SMG, Shotgun, Rifle, GrenadeLauncher};

    private Dictionary<Weapons, float[]> weaponDictionary = new Dictionary<Weapons, float[]>();
    private Dictionary<Weapons, bool> hasWeaponDictionary = new Dictionary<Weapons, bool>();

    // Weapon values will go with the following order reload speed, damage, shotspeed, knockback, spread, range.
    //                                  Reload Speed    Damage          Shotspeed       Knockback       Spread      Range
    private float[] pistolValues =      {1              ,2              ,3              ,4              ,1          ,100            };
    private float[] swordValues =       {1              ,2              ,3              ,4              ,1          ,100            };
    private float[] sMGValues =         {1              ,2              ,3              ,4              ,1          ,100            };
    private float[] shotgunValues =     {1              ,2              ,3              ,4              ,1          ,100            };
    private float[] rifleValues =       {1              ,2              ,3              ,4              ,1          ,100            };
    private float[] grenadeValues =     {1              ,2              ,3              ,4              ,1          ,100            };

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
        hasWeaponDictionary.Add(Weapons.SMG, false);
        hasWeaponDictionary.Add(Weapons.Shotgun, false);
        hasWeaponDictionary.Add(Weapons.Rifle, false);
        hasWeaponDictionary.Add(Weapons.GrenadeLauncher, false);
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

}
