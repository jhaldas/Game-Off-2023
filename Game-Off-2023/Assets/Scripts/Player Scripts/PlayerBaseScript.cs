using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseScript : MonoBehaviour, IOnHit
{
    public static PlayerBaseScript playerInstance;
    [SerializeField] private float playerXP = 0;
    [SerializeField] private float playerXPCap = 100;
    [SerializeField] private float playerScales = 0;
    [SerializeField] private float playerHealth = 100;
    [SerializeField] private float playerMaxHealth = 100;
    [SerializeField] private Rigidbody2D playerRigidbody;
    
    public enum PlayerValueOptions
    {
        Set         = 0,
        Add         = 1,
        Multiply    = 2
    };

    void Awake()
    {
        if(playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {

    }
    
    void Update()
    {
        PlayerHealthUpdate();
    }

    public void OnHealthChange(float damage)
    {
        playerHealth += damage;
    }

    public void OnKnockback(float knockback, Vector2 direction)
    {
        playerRigidbody.AddForce(direction * knockback, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Checks for when the player is dead
    /// </summary>
    private void PlayerHealthUpdate()
    {
        if(playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Change player's max health.
    /// </summary>
    /// <param name="maxHealthValue">Set the float value</param>
    /// <param name="option">Options: Set, Add, Multiply</param>
    public void ChangePlayerMaxHealth(float maxHealthValue, PlayerValueOptions option)
    {
        if(option == PlayerValueOptions.Set)
        {
            playerMaxHealth = maxHealthValue;
        }
        else if(option == PlayerValueOptions.Add)
        {
            playerMaxHealth += maxHealthValue;
        }
        else if(option == PlayerValueOptions.Multiply)
        {
            playerMaxHealth *= maxHealthValue;
        }
        else
        {
            Debug.Log("Option not valid.");
        }
    }

    /// <summary>
    /// Change player's health.
    /// </summary>
    /// <param name="healthValue">Set the float value</param>
    /// <param name="option">Options: Set, Add, Multiply</param>
    public void ChangePlayerHealth(float healthValue, PlayerValueOptions option)
    {
        if(option == PlayerValueOptions.Set)
        {
            playerHealth = healthValue;
        }
        else if(option == PlayerValueOptions.Add)
        {
            playerHealth += healthValue;
        }
        else if(option == PlayerValueOptions.Multiply)
        {
            playerHealth *= healthValue;
        }
        else
        {
            Debug.Log("Option not valid.");
        }
    }

    /// <summary>
    /// Change player's scales.
    /// </summary>
    /// <param name="scaleValue">Set the float value</param>
    /// <param name="option">Options: Set, Add, Multiply</param>
    public void ChangePlayerScales(float scaleValue, PlayerValueOptions option)
    {
        if(option == PlayerValueOptions.Set)
        {
            playerScales = scaleValue;
        }
        else if(option == PlayerValueOptions.Add)
        {
            playerScales += scaleValue;
        }
        else if(option == PlayerValueOptions.Multiply)
        {
            playerScales *= scaleValue;
        }
        else
        {
            Debug.Log("Option not valid.");
        }
    }

    /// <summary>
    /// Change player's xp.
    /// </summary>
    /// <param name="playerXPValue">Set the float value</param>
    /// <param name="option">Options: Set, Add, Multiply</param>
    public void ChangePlayerXP(float playerXPValue, PlayerValueOptions option)
    {
        if(option == PlayerValueOptions.Set)
        {
            playerXP = playerXPValue;
        }
        else if(option == PlayerValueOptions.Add)
        {
            playerXP += playerXPValue;
        }
        else if(option == PlayerValueOptions.Multiply)
        {
            playerXP *= playerXPValue;
        }
        else
        {
            Debug.Log("Option not valid.");
        }
    }
}
