using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput playerInput;
    public GameObject avatar;
    public float walkSpeed = 3f;
    public float rotationSpeed = 10f;
    public float gravity = 10f;
    private float currentRotationVelocity = 0.0f;
    private Vector3 currentRotationVelocityV = Vector3.zero;
    private CharacterController characterController;
    private float initialY;
    private Camera camera;

    void Awake()
    {
        this.characterController = this.GetComponent<CharacterController>();
        this.camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector3 direction = this.playerInput.direction * walkSpeed * Time.deltaTime;
        if (this.characterController.isGrounded)
        {
            Vector3 newDir = Vector3.RotateTowards(avatar.transform.forward, this.playerInput.direction, rotationSpeed, 0.0f);
            //newDir += Vector3.RotateTowards(transform.forward, camera.transform.forward, rotationSpeed, 0.0f);
            //Debug.Log("Rotate towards: " + newDir);
            // rotate character
            avatar.transform.rotation = Quaternion.LookRotation(newDir);
            // move character
        }
        direction.y -= gravity * Time.deltaTime;

        this.characterController.Move(direction);

        if (playerInput.action)
        {
            GetComponent<Disk>().BeginThrow();
        }
       // Debug.Log("Is grounded: " + this.characterController.isGrounded);
    }
}