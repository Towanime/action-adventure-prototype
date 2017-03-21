using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterActivator : BaseActivator {
    public int requiredActivations;
    public int currentActivations;
    public BaseActivator activator;


    public override void Activate(GameObject trigger)
    {
        currentActivations++;
        Debug.Log("Activate! " + currentActivations);
        if (currentActivations >= requiredActivations)
        {
            activator.Activate(gameObject);
        }
    }

    public override void Desactivate()
    {
        currentActivations--;
        Debug.Log("Desactivate! " + currentActivations);
        if (currentActivations < requiredActivations)
        {
            activator.Desactivate();
        }
    }
}
