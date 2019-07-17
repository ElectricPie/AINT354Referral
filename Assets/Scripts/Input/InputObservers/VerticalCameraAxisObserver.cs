using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalCameraAxisObserver : AxisCameraObserver
{
    public VerticalCameraAxisObserver() : base() { }
    public VerticalCameraAxisObserver(CameraController cameraController) : base(cameraController) { }

    public override void OnInputEvent(float value)
    {
        m_cameraController.MoveCameraVertically(value);
    }
}
