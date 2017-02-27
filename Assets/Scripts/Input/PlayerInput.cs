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
    [Tooltip("Rotation from the mouse to apply on the camera.")]
    public Vector3 rotation;
    /// <summary>
    /// True if the player pressed the action key in this frame.
    /// </summary>
    public bool action;
    /// <summary>
    /// True if the player is holding the run key.
    /// </summary>
    public bool run;
    private Vector3 cameraDirection;

    void Update()
    {
        // update values depending on the input
        this.SetDirection();
        this.SetRotation();
        this.SetAction();
        this.SetRun();
    }

    private void SetDirection()
    {
        // merge these vars later
        this.direction = Vector3.zero;
        this.cameraDirection = Vector3.zero;

        if (Input.GetKey(this.config.forward))
        {
            this.direction += transform.forward;
            this.cameraDirection += cameraAnchor.transform.forward;
        }
        else if (Input.GetKey(this.config.backwards))
        {
            this.direction -= transform.forward;
            this.cameraDirection -= cameraAnchor.transform.forward;
        }

        if (Input.GetKey(this.config.left))
        {
            this.direction -= transform.right;
            this.cameraDirection -= cameraAnchor.transform.right;
        }
        else if (Input.GetKey(this.config.right))
        {
            this.direction += transform.right;
            this.cameraDirection += cameraAnchor.transform.right;
        }
        this.direction = this.cameraDirection.normalized;
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

    private void SetAction()
    {
        this.action = Input.GetKeyDown(this.config.action);
    }

    private void SetRun()
    {
        this.run = Input.GetKey(this.config.run);
    }
}
