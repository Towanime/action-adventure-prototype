﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class ManipulableObject : MonoBehaviour
{
    public GameObject target;
    public GameObject platform;
    public GameObject player;
    // 0 = none, 1 = tile, 2 = cube
    public int initialState = 0;
    public Vector3[] scales = new Vector3[] { new Vector3(1f, 0.5f, 1f), new Vector3(4f, 0.5f, 4f), new Vector3(4f, 4.5f, 4f) };
    public Vector3[] positions = new Vector3[] { Vector3.zero, Vector3.zero, new Vector3(0, 2f, 0) };
    private int currentState;
    private Vector3 initialPosition;
    // true if it's done changing the scale and position
    private bool isScaleDone;
    private bool isMovementDone;

    void Start()
    {
        currentState = initialState;
        initialPosition = target.transform.localPosition;
        SetScale(false);
    }

    public void Grow()
    {
        // max two
        int nextState = Mathf.Min(currentState + 1, 2);
        if (currentState != nextState)
        {
            currentState = nextState;
            SetScale();
        }
    }

    public void Shrink()
    {
        int nextState = Mathf.Max(currentState - 1, 0);
        if (currentState != nextState)
        {
            currentState = nextState;
            SetScale();
        }
    }

    private void SetScale(bool animate = true)
    {
        if (animate)
        {
            // if it's the last scale move y as well here
            isScaleDone = false;
            isMovementDone = false;
            target.gameObject.Tween("ScaleObject", target.transform.localScale,
                scales[currentState], 0.3f, TweenScaleFunctions.CubicEaseIn,
                (t) =>
                {
                    // progress
                    target.transform.localScale = t.CurrentValue;
                },
                (t) =>
                {
                    isScaleDone = true;
                });
            // position 
            platform.gameObject.Tween("MoveObject", platform.transform.localPosition,
                positions[currentState], 0.3f, TweenScaleFunctions.CubicEaseIn,
                (t) =>
                {
                    // progress
                    //.transform.localPosition = t.CurrentValue;
                    platform.transform.localPosition = t.CurrentValue;
                },
                (t) =>
                {
                    isMovementDone = true;
                });
            /*target.gameObject.Tween("MoveObject", target.transform.localPosition,
                positions[currentState], 0.3f, TweenScaleFunctions.CubicEaseIn,
                (t) =>
                {
                    // progress
                    target.transform.localPosition = t.CurrentValue;
                },
                (t) =>
                {
                    isMovementDone = true;
                });*/

            /* target.gameObject.Tween("MovePlatform", platform.transform.localPosition,
                 positions[currentState], 0.3f, TweenScaleFunctions.CubicEaseIn,
                 (t) =>
                 {
                     // progress
                     platform.transform.localPosition = t.CurrentValue;
                 },
                 (t) =>
                 {
                     isMovementDone = true;
                 });*/
        }
        else
        {
            // set it right away
            target.transform.localScale = scales[currentState];
            target.transform.localPosition = positions[currentState];
        }

    }


    public void OnTriggerExit(Collider other)
    {
        if(player != null){
            player.transform.parent = null;
            player = null;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            player.transform.parent = platform.transform;
        }
    }

    public bool IsDone()
    {
        return isScaleDone && isMovementDone;
    }
}
