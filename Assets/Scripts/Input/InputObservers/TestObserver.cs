using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObserver : InputObserver
{
    public override void OnInputEvent(float value)
    {
        Debug.Log("Triggered");
    }
}

