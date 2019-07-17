using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AxisCameraObserver : InputObserver
{
    protected CameraController m_cameraController;

    public AxisCameraObserver()
    {
        m_cameraController = null;
    }

    public AxisCameraObserver(CameraController camera)
    {
        m_cameraController = camera;
    }
}
