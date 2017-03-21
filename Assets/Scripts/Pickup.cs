using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    /**
     * List to hold activators to call when this switch is "activated".
     * */
    public List<BaseActivator> activators;

    public void OnTriggerEnter(Collider other)
    {
        foreach (BaseActivator activator in activators)
        {
            activator.Activate(gameObject);
        }
        Destroy(gameObject);
    }
}
