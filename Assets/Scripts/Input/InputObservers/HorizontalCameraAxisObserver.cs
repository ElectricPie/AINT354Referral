using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCameraAxisObserver : AxisCameraObserver
{
    public HorizontalCameraAxisObserver() : base() { }
    public HorizontalCameraAxisObserver(CameraController cameraController) : base(cameraController) { }

    public override void OnInputEvent(float value)
    {
        m_cameraController.transform.position += m_cameraController.transform.right * value * 0.1f;
    }
}
