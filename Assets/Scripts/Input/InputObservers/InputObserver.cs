using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputObserver 
{
    public abstract void OnInputEvent(float value);
}