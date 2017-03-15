using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Tooltip("Current key configuration.")]
    public KeyboardMouseConfig config;
    [Tooltip("Game object that will be used to know where the camera is facing.")]
    public GameObject cameraAnchor;
    [Tooltip("Direction to where the player will move next.")]
    public Vector3 direction;
    [Tooltip("The first direction detected without merging multiple orientations.")]
    public Vector3 rawDirection;
    [Tooltip("Rotation from the mouse to apply on the camera.")]
    public Vector3 rotation;
    /// <summary>
    /// True if the player pressed the attack key in this frame.
    /// </summary>
    public bool attack;
    public bool disk;
    public bool architectMode;
    public bool holdingAction;
    private Vector3 cameraDirection;

    void Update()
    {
        // update values depending on the input
        this.SetDirection();
        this.SetRawDirection();
        this.SetRotation();
        this.SetActions();
    }

    private void SetDirection()
    {
        // merge these vars later
        this.direction = Vector3.zero;
        this.cameraDirection = Vector3.zero;

        if (Input.GetKey(this.config.forward))
        {
            this.cameraDirection += cameraAnchor.transform.forward;
        }
        else if (Input.GetKey(this.config.backwards))
        {
            this.cameraDirection -= cameraAnchor.transform.forward;
        }

        if (Input.GetKey(this.config.left))
        {
            this.cameraDirection -= cameraAnchor.transform.right;
        }
        else if (Input.GetKey(this.config.right))
        {
            this.cameraDirection += cameraAnchor.transform.right;
        }
        this.direction = this.cameraDirection.normalized;
    }

    private void SetRawDirection()
    {
        // merge these vars later
        this.rawDirection = Vector3.zero;

        if (Input.GetKey(this.config.forward))
        {
            this.rawDirection = cameraAnchor.transform.forward;
        }
        else if (Input.GetKey(this.config.backwards))
        {
            this.rawDirection -= cameraAnchor.transform.forward;
        }else if (Input.GetKey(this.config.left))
        {
            this.rawDirection -= cameraAnchor.transform.right;
        }
        else if (Input.GetKey(this.config.right))
        {
            this.rawDirection = cameraAnchor.transform.right;
        }
        this.rawDirection = this.rawDirection.normalized;
    }

    private void SetRotation()
    {
        float yaw = Input.GetAxis("Mouse X") * this.config.mouseXSensitivity;
        float pitch = Input.GetAxis("Mouse Y") * this.config.mouseYSensitivity;

        if (this.config.invertY)
        {
            pitch *= -1;
        }

        this.rotation = new Vector3(yaw, pitch, 0f);
    }

    private void SetActions()
    {
        this.attack = Input.GetKeyDown(this.config.attack);
        this.disk = Input.GetKeyDown(this.config.disk);
        this.architectMode = Input.GetKeyDown(this.config.architectMode);
        this.holdingAction = Input.GetKey(this.config.attack);
    }    
}
