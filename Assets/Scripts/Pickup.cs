using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    bool triggered;

    /**
     * List to hold activators to call when this switch is "activated".
     * */
    public List<BaseActivator> activators;

    public void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            foreach (BaseActivator activator in activators)
            {
                activator.Activate(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
