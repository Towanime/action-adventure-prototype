using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour {
    public GameObject player;
    public GameObject platform;
    private Collider platformCollider;

    void Start()
    {
        platformCollider = platform.GetComponent<Collider>();
        platformCollider.enabled = false;
    }

    public void OnTriggerExit(Collider other)
    {
        if (player != null)
        {
            player.transform.parent = null;
            player = null;
            platformCollider.enabled = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            player.transform.parent = platform.transform;
            platformCollider.enabled = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (player != null && other.CompareTag("Player"))
        {
            player = other.gameObject;
            player.transform.parent = platform.transform;
         //   platformCollider.enabled = true;
        }
    }
}
