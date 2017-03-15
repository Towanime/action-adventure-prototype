using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manipulator : MonoBehaviour {
    public PlayerInput playerInput;
    public float maxDistance = 7;
    public bool isDisabled;
    private GameObject target;
    private ManipulableObject targetComponent;
    private bool inSight;
    private bool isDone;
    private Camera camera;

    void Start()
    {
        camera = GetComponentInChildren<Camera>();
        isDone = true;
    }

    void Update()
    {
        DetectTarget();
        // update done variable
        if (targetComponent != null)
        {
            isDone = targetComponent.IsDone();
        }
        else
        {
            isDone = true;
        }
    }

    public void Activate()
    {
        if (isDisabled) return;

        if (target)
        {
            isDone = false;
            // grow or shrink
            if (playerInput.attack)
            {
                targetComponent.Grow();
            }else if (playerInput.disk)
            {
                targetComponent.Shrink();
            }

            isDone = targetComponent.IsDone();
            /*if (!grabbing)
            {
                // first check if it's attached to a switch and detach it
                Switch switchTile = target.GetComponentInParent<Switch>();
                if (switchTile)
                {
                    switchTile.Detach();
                }
                // attach to avatar
                target.transform.parent = gameObject.transform;
                // make it kinematic
                objectRigidBody = target.GetComponent<Rigidbody>();
                objectRigidBody.isKinematic = true;
                objectRigidBody.useGravity = false;
                // change position of the grabbed object
                target.transform.position = gameObject.transform.position;
                target.transform.localPosition = new Vector3(1.5f, 0.5f, 2);
                grabbing = true;
                inSight = false;
            }
            else
            {
                // extra reycast to check if it's looking into a switch to auto attach
                RaycastHit hit;
                Ray ray = getCameraRay();
                objectRigidBody.useGravity = true;
                // Cast a ray
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Switch"))
                {
                    Switch script = hit.collider.gameObject.GetComponent<Switch>();
                    // if it's null it might be in the parent! (It's pointing to the center of the switch which is another object)
                    script = hit.collider.gameObject.GetComponentInParent<Switch>();
                    // attach it? if returns false then the switch is already taken so it's ignored
                    if (!script.Attach(target)) return;
                    afterRelease();
                }
                else
                {
                    release();
                }
            }*/
        }
    }

    private Ray getCameraRay()
    {
        return new Ray(camera.transform.position, camera.transform.forward);
    }

    private void DetectTarget()
    {
        RaycastHit hit;
        Ray ray = getCameraRay();

        if (Physics.Raycast(ray, out hit) && maxDistance >= hit.distance &&
            hit.collider.gameObject.CompareTag("Manipulable"))
        {
            target = hit.collider.gameObject;
            targetComponent = target.GetComponent<ManipulableObject>();
            inSight = true;
        }
       else
        {
            target = null;
            inSight = false;
        }
    }

    public bool InSight()
    {
        return inSight;
    }

    public bool IsDone()
    {
        return isDone;
    }
    
    public bool Disabled
    {
        get { return this.isDisabled; }
        set { this.isDisabled = value; }
    }
}
