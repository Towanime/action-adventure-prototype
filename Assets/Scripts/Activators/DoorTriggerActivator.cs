using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerActivator : MonoBehaviour
{
    /// <summary>
    /// Activator to turn on/off.
    /// </summary>
    public BaseActivator activator;
    public int requiredKeys;
    private KeyCollector collector;

    void Start()
    {
        collector = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyCollector>();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (collector.currentKeys >= requiredKeys)
        {
            activator.Activate(gameObject);
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        activator.Desactivate();
    }
}