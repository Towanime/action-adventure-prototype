using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput playerInput;
    [Tooltip("Player avatar that will be rotated.")]
    public GameObject avatar;
    [Tooltip("Moving speed for the avatar.")]
    public float walkSpeed = 10f;
    [Tooltip("Max vertical speed to apply to the player when they are not touching the ground.")]
    public float maxVerticalSpeed = 3f;
    [Tooltip("Gravity for the player avatar.")]
    public float gravity = 2f;
    [Tooltip("Rotation rate between directions.")]
    public float rotationSpeed = 300f;
    [Tooltip("Used to speed up the rotation when the new direction is opposite to the current one.")]
    public float rotationOppositeDirectionModifier = 2f;
    public GroundCheck groundCheck;
    public Animator animator;
    public bool isDisabled;
    private CharacterController characterController;
    // after blink or normal move calculations
    private float movementSpeed;
    // rotation variables
    private Quaternion targetRotation;
    private Vector3 currentDirection = Vector3.forward;
    private Vector3 nextDirection = Vector3.forward;
    private bool applyRotationModifier;
    // blink trail variables
    // initial time for the trail
    private float originalTrailDuration;

    void Awake()
    {
        this.characterController = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (isDisabled) return;
        nextDirection = this.playerInput.direction;
        // check if the avatar needs to rotate
        RotationUpdate();
        // check movement and blink ability
        MovementSpeedUpdate();
        // apply gravity
        VerticalSpeedUpdate();
        // move the direction after all the calculations
        Vector3 direction = nextDirection * movementSpeed;
        this.characterController.Move(direction);
    }

    void VerticalSpeedUpdate()
    {
        if (!groundCheck.IsGrounded)
        {
            nextDirection.y = Mathf.Clamp(nextDirection.y - gravity, -maxVerticalSpeed, maxVerticalSpeed);
        }
    }

    /// <summary>
    /// Modifies movemend speed if the blink is active.
    /// </summary>
    void MovementSpeedUpdate()
    {
        /*if (this.playerInput.blink)
        {
            // add blink speed and show trail!
            movementSpeed = walkSpeed * blinkDistance * Time.deltaTime;
            blinkTrailRenderer.time = originalTrailDuration;
        }
        else
        {*/
        // normal walk speed 
        movementSpeed = walkSpeed * Time.deltaTime;
        // }
    }

    /// <summary>
    /// Checks if the player avatar should face a new direction.
    /// </summary>
    void RotationUpdate()
    {
        // only start rotation if the player inputs a new direction
        if (currentDirection != nextDirection && nextDirection != Vector3.zero)
        {
            // check if the directions are opposite and need to rotate faster
            applyRotationModifier = currentDirection == -nextDirection;
            // set new rotation target
            currentDirection = nextDirection;
            targetRotation = Quaternion.LookRotation(currentDirection);
        }
        // rotate if needed
        if (avatar.transform.rotation != targetRotation)
        {
            avatar.transform.rotation = Quaternion.RotateTowards(avatar.transform.rotation, targetRotation,
                rotationSpeed * (applyRotationModifier ? rotationOppositeDirectionModifier : 1) * Time.deltaTime);
        }
    }

    public bool Disabled
    {
        get { return this.isDisabled; }
        set { this.isDisabled = value; }
    }
}