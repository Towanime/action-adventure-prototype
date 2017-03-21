﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActivator : BaseActivator {
    public BaseActivator activator;


    public override void Activate(GameObject trigger)
    {
        activator.Activate(gameObject);
    }

    public override void Desactivate()
    {
        activator.Desactivate();
    }
}