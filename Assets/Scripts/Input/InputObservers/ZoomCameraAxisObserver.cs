using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCameraAxisObserver : AxisCameraObserver
{
    public ZoomCameraAxisObserver() : base() { }
    public ZoomCameraAxisObserver(CameraController cameraController) : base(cameraController) { }

    public override void OnInputEvent(float value)
    {
        m_cameraController.ZoomCamera(value);
    }
}
