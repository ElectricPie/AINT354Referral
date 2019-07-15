using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCameraAxisObserver : InputObserver
{
    private GameObject m_camera;

    public HorizontalCameraAxisObserver()
    {
        m_camera = null;
    }

    public HorizontalCameraAxisObserver(GameObject camera)
    {
        m_camera = camera;
    }

    public override void OnInputEvent(float value)
    {
        m_camera.transform.position += m_camera.transform.right * value * 0.1f;
    }
}
