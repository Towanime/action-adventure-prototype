using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manipulator : MonoBehaviour {
    public PlayerInput playerInput;
    public float maxDistance = 7;
    public bool isDisabled;
    public AudioClip sfx;
    private GameObject target;
    private ManipulableObject targetComponent;
    private bool inSight;
    private bool isDone;
    private Camera camera;
    private AudioSource audioSource;

    void Start()
    {
        camera = GetComponentInChildren<Camera>();
        isDone = true;
        audioSource = GetComponent<AudioSource>();
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
                PlaySfx();
            }else if (playerInput.disk)
            {
                targetComponent.Shrink();
                PlaySfx();
            }

            isDone = targetComponent.IsDone();
        }
    }

    private void PlaySfx()
    {
        audioSource.clip = sfx;
        audioSource.Play();
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
