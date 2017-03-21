using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToActivator : BaseActivator
{
    public GameObject toMove;
    public Transform desiredPosition;
    public float speed;
    public bool returnToOrigin;
    public float waitTime;
    private Vector3 origin;
    private Vector3 target;
    private bool returning;

    void Start()
    {
        origin = toMove.transform.position;
        target = desiredPosition.transform.position;
    }

    void FixedUpdate()
    {
        if (active)
        {
            Vector3 nextPosition = Vector3.MoveTowards(toMove.transform.position, target, speed * Time.fixedDeltaTime);
            toMove.transform.Translate(nextPosition - toMove.transform.position);

            if (toMove.transform.position == target)
            {
                Debug.Log("Destination reached!");
                returning = !returning;
                if (returning)
                {
                    target = origin;
                }else
                {
                    target = desiredPosition.transform.position;
                }
                // make it wait
                active = false;
                Invoke("Restart", waitTime);
            }
        }
        // turns off until the trigger activates it again.
        //active = false;
    }

    private void Restart()
    {
        active = true;
    }
}