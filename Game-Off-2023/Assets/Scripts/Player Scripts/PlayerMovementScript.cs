using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    //public static PlayerMovementScript nstance;
    private Vector2 playerInputMovementDirection = Vector2.zero;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float playerDrag = 10f;
    [SerializeField] private float playerMaxSpeed = 5f;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private Animator _playerAnimator;
    private bool facingRight = true;


    private void Awake()
    {
        
    }
    
    void Update()
    {
        UpdatePlayerMove();
    }

    /// <summary>
    /// Takes player input from actions and converts it to a Vector2 direction.
    /// </summary>
    public void OnMovePlayer(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Started || context.action.phase == InputActionPhase.Performed)
        {
            playerInputMovementDirection = context.action.ReadValue<Vector2>(); // Updates player input direction while buttons are being pressed.
        }
        if(context.action.phase == InputActionPhase.Canceled)
        {
            playerInputMovementDirection = Vector2.zero; // Makes sure player input is set to zero when not holding a direction.
        }
        
    }

    /// <summary>
    /// Handles the rigidbody movement of the player for the update function.
    /// </summary>
    private void UpdatePlayerMove()
    {
        if(playerInputMovementDirection != Vector2.zero)
        {
            if (!(playerRigidbody.velocity.magnitude >= playerMaxSpeed))
            {
                playerRigidbody.velocity += playerInputMovementDirection * playerSpeed * Time.deltaTime;
            }
            else 
            {
                playerRigidbody.velocity = playerRigidbody.velocity;
            }
        }

    }

    /// <summary>
    /// Function for other classes to call to change move speed by adding a value.
    /// </summary>
    /// <param name="speed">Amount of speed to add to the player.</param>
    public void OnChangePlayerSpeed(float speed)
    {
        playerSpeed += speed;
    }

    /// <summary>
    /// Get player move speed for other functions.
    /// </summary>
    /// <returns>Player Speed.</returns>
    public float ReturnMoveSpeed()
    {
        return playerSpeed;
    }

    /// <summary>
    /// This function will be used to handle setting the drag..
    /// </summary>
    /// <param name="drag">Value to set the drag.</param>
    public void OnSetPlayerDrag(float drag) // Used for when player is on different tiles.
    {
        playerRigidbody.drag = drag; // default is 10
    }

    /// <summary>
    /// Changes the maximum speed of the player
    /// </summary>
    /// <param name="newMaxSpeed">New Player Maximum Speed</param>
    public void SetPlayerMaxSpeed(float newMaxSpeed) 
    {
        playerMaxSpeed = newMaxSpeed; 
    }

    /// <summary>
    /// Increases or decreases max speed by given amount
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeSpeedByAmount(float amount) 
    {
        playerSpeed += amount;
        playerMaxSpeed += amount;
    }

    /// <summary>
    /// Returns the max speed of the player
    /// </summary>
    /// <returns>Players Current Max Speed</returns>
    public float GetPlayerMaxSpeed()
    {
        return playerMaxSpeed;
    }

    /// <summary>
    /// Gets Player Drag
    /// </summary>
    /// <returns>Player Drag</returns>
    public float GetPlayerDrag() 
    {
        return playerDrag;
    }

    public void HandleAnimator()
    {
        if (playerInputMovementDirection.magnitude > 0)
        {
            _playerAnimator.SetBool("IsMoving", true);
        }
        else 
        {
            _playerAnimator.SetBool("isMoving", false);
        }
        if (playerInputMovementDirection.y > 0)
        {
            _playerAnimator.SetBool("FacingFront", false);
        }
        else
        {
            _playerAnimator.SetBool("FacingFront", true);
        }
    }

    public void HandlePlayerFlip() 
    {
        if (playerInputMovementDirection.x > 0)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }
    }

}

/**
if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
**/