using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public GameObject avatar;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object: " + other.name);
        Vector3 relativePos = other.transform.position - avatar.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        avatar.transform.rotation = rotation;
    }

    public void OnTriggerStay(Collider other)
    {

    }

    public void OnTriggerExit(Collider other)
    {

    }
}
