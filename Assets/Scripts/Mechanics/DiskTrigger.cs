using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskTrigger : MonoBehaviour
{
    public Disk disk;

    public void OnCollisionEnter(Collision collision)
    {
        if (!disk.Collided)
        {
            disk.OnCollision(collision);
        }
    }

    public void OnCollisionExit(Collision collision)
    {

    }
}
