using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickupActivator : BaseActivator
{
    private KeyCollector collector;

    void Start()
    {
        collector = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyCollector>();
    }

    public override void Activate(GameObject trigger)
    {
        //SoundManager.Instance.StopAndPlay(SoundManager.Instance.bigCoinPickupSound);

        if (collector)
        {
            collector.PickupKey();
        }
    }
}