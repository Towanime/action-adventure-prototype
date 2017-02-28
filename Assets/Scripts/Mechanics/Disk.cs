using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    private enum DiskState { Default, WaitingAnimation, Thrown, Returning };

    [Tooltip("Hand on the model from where the throw will begin.")]
    public GameObject anchor;
    /// <summary>
    /// Used for the throw calculations
    /// </summary>
    private GameObject target;
    [Tooltip("Character to where the disk will return.")]
    public GameObject avatar;
    [Tooltip("Hidden disk to show when throwing but not during the animations.")]
    public GameObject disk;
    [Tooltip("Disk on the character model that will be hidding during the throw.")]
    public GameObject dummyDisk;
    [Tooltip("How far in Z the disk will go.")]
    public float distance;
    [Tooltip("How fast the disk will go.")]
    public float speed;
    [Tooltip("How fast the disk will return.")]
    public float returnSpeed;
    public Animator animator;
    // starting throw time
    private Vector3 startPoint;
    private float startTime;
    private float journeyLength;
    private bool thrown;
    private bool returning;
    private DiskState currentState;

    void Start()
    {
        target = new GameObject();
    }

    void Update()
    {

        switch (currentState)
        {
            case DiskState.Thrown:
                float distCovered = (Time.time - startTime) * speed;
                float fracJourney = distCovered / journeyLength;
                // update position of the disk
                disk.transform.position = Vector3.Lerp(startPoint, target.transform.position, fracJourney);
                // got to the target?
                if (fracJourney >= 1)
                {
                    Comeback();
                    //Debug.Log("Arm reached max distance! At: " + disk.transform.position.ToString());
                }
                break;

            case DiskState.Returning:
                //journeyLength = Vector3.Distance(target.transform.position, anchor.transform.position);
                distCovered = (Time.time - startTime) * returnSpeed;
                fracJourney = distCovered / journeyLength;
                disk.transform.position = Vector3.Lerp(target.transform.position, avatar.transform.position, fracJourney);
                // check destination
                if (fracJourney >= 1)
                {
                    // finish grab
                    End();
                }
                break;
        }
    }

    public void BeginThrow()
    {
        currentState = DiskState.WaitingAnimation;
        animator.SetTrigger("Throw");
    }

    public void Throw()
    {
        //if (currentState != DiskState.WaitingAnimation) return;
        // start throw
        currentState = DiskState.Thrown;
        startTime = Time.time;
        Vector3 targetPosition = anchor.transform.position;
        targetPosition.z += distance;
        target.transform.position = targetPosition;
        disk.transform.position = anchor.transform.position;
        disk.transform.rotation = anchor.transform.rotation;
        startPoint = anchor.transform.position;
        journeyLength = Vector3.Distance(anchor.transform.position, target.transform.position);
        // turn off/on renderers
        disk.GetComponent<Renderer>().enabled = true;
        dummyDisk.GetComponent<Renderer>().enabled = false;
    }

    private void Comeback()
    {
        // start returning with delay
        currentState = DiskState.Returning;
        startTime = Time.time;
        startPoint = target.transform.position;
        journeyLength = Vector3.Distance(startPoint, anchor.transform.position);
        //arm.GetComponent<Collider>().enabled = false;
    }

    private void End()
    {
        currentState = DiskState.Default;
        startTime = 0;
        disk.GetComponent<Renderer>().enabled = false;
        dummyDisk.GetComponent<Renderer>().enabled = true;
        // begin cooldown

    }

    /* IEnumerator Throw(float dist, float width, Vector3 direction, float time)
     {
         Vector3 pos = anchor.transform.position;
         float height = anchor.transform.position.y;
         Quaternion q = Quaternion.FromToRotation(Vector3.forward, direction);
         float timer = 0.0f;
         //rigidbody.AddTorque(0.0f, 400.0f, 0.0f);
         while (timer < time)
         {
             float t = Mathf.PI * 2.0f * timer / time - Mathf.PI / 2.0f;
             float x = width * Mathf.Cos(t);
             float z = dist * Mathf.Sin(t);
             Vector3 v = new Vector3(x, height, z + dist);
             //rigidbody.MovePosition(pos + (q * v));
             transform.Translate(pos + (q * v));
             timer += Time.deltaTime;
             yield return null;
         }

         transform.LookAt(anchor.transform);
         //transform.Translate();

         /*rigidbody.angularVelocity = Vector3.zero;
         rigidbody.velocity = Vector3.zero;
         rigidbody.rotation = Quaternion.identity;
         rigidbody.MovePosition(anchor.transform.position);*/

}
