using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public AudioClip pickupSfx;
    private bool triggered;
    private AudioSource audioSource;
    public BaseActivator pickupActivator;
    private KeyCollector collector;

    void Start()
    {
        collector = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyCollector>();
        audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    /**
     * List to hold activators to call when this switch is "activated".
     * */
    public List<BaseActivator> activators;

    public void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            triggered = true;
            collector.PickupKey();
            /* foreach (BaseActivator activator in activators)
             {
                 activator.Activate(gameObject);
             }*/
            //pickupActivator.Activate(gameObject);
            Destroy(gameObject);
            audioSource.clip = pickupSfx;
            audioSource.Play();
        }
    }
}
