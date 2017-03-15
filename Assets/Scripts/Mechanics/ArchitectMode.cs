using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class ArchitectMode : MonoBehaviour {
    // controller for the scan disk
    public CharacterController diskCharacterController;
    public Camera playerCamera;
    public Camera scannerCamera;
    public GameObject playerCameraX;
    public ScanLaser scanner;
    public GameObject scanDiskSpawn;
    public PlayerInput playerInput;
    [Tooltip("Moving speed for the avatar.")]
    public float speed = 6f;
    public GameObject scanDisk;
    public bool isDisabled;
    private Vector3 nextDirection = Vector3.forward;
    // block movement variables
    private GameObject target;
    // true if a block is moving and the disc should not move
    private bool waiting;

    void Update()
    {
        if (isDisabled) return;
        nextDirection = this.playerInput.direction;

        // check if there is a object selected
        if (scanner.GetTarget() != null && playerInput.attack) // there is a target and player selects it
        {
            target = scanner.GetTarget();
            // center disk with the target
            Vector3 newPositon = target.GetComponent<Renderer>().bounds.center;
            newPositon.y = scanDisk.transform.position.y;
            scanDisk.transform.position = newPositon;

        } else if (target != null && !waiting && playerInput.rawDirection != Vector3.zero) // if there is a target move that object
        {
            MoveObject();
        }
        else if (target == null)
        {
            // no target? move the disk!
            MoveScanner();
        }
        /*
                // move the direction after all the calculations
                if (!waiting && scanner.GetTarget() != null && playerInput.holdingAction)
                {
                    waiting = true;
                    GameObject target = scanner.GetTarget();
                    //Debug.Log(playerInput.rawDirection);
                    //target.transform.position = target.transform.position + nextDirection.normalized;
                    target.gameObject.Tween("MoveObject", target.transform.position,
                        target.transform.position + playerInput.rawDirection, 0.3f, TweenScaleFunctions.CubicEaseIn,
                        (t) =>
                        {
                            // progress
                            target.transform.position = t.CurrentValue;
                            //Debug.Log("Progress?? " + t.CurrentValue);
                        }, 
                        (t) =>
                        {
                            waiting = false;
                        });
                }
                if(!waiting)
                {
                    Vector3 direction = nextDirection * (speed * Time.deltaTime);
                    this.diskCharacterController.Move(direction);
                }*/
        //Debug.Log("Direction: " + nextDirection.normalized);
    }

    private void MoveObject()
    {
        if (waiting) return;
        waiting = true;
        target.gameObject.Tween("MoveObject", target.transform.position,
            target.transform.position + playerInput.rawDirection, 0.3f, TweenScaleFunctions.CubicEaseIn,
            (t) =>
            {
                // progress
                target.transform.position = t.CurrentValue;
                Debug.Log("Progress?? " + t.CurrentValue);
            },
            (t) =>
            {
                waiting = false;
                //target = null;
            });
    }

    private void MoveScanner()
    {
        Vector3 direction = nextDirection * (speed * Time.deltaTime);
        this.diskCharacterController.Move(direction);
    }

    public void Activate()
    {
        target = null;
        isDisabled = false;
        //scanner.gameObject.transform.forward = playerCamera.transform.forward.normalized;
        Vector3 rotation = scanDisk.transform.localEulerAngles;
        rotation.y = playerCameraX.transform.localEulerAngles.y;
        Debug.Log("Disk rotation: " + rotation);
        scanDisk.transform.rotation = Quaternion.Euler(rotation);
        scanDisk.transform.position = scanDiskSpawn.transform.position;
        scanDisk.SetActive(true);
        scanner.Activate();
        // disable main camera
        playerCamera.enabled = false;
        scannerCamera.enabled = true;
        Debug.Log("Camera foward: " + playerCamera.transform.forward);
    }

    public void Deactivate()
    {
        scanDisk.SetActive(false);
        isDisabled = true;
        scanner.Deactivate();
        // disable main camera
        playerCamera.enabled = true;
        scannerCamera.enabled = false;
    }

    public bool Disabled
    {
        get { return this.isDisabled; }
        //set { this.isDisabled = value; }
    }
}
