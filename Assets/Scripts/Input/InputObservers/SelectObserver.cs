using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObserver : InputObserver
{
    //Private
    private PlayerController m_playerController;

    public SelectObserver()
    {
        m_playerController = null;
    }

    public SelectObserver(PlayerController playerController) 
    {
        m_playerController = playerController;
    }

    public override void OnInputEvent(float value)
    {
        if (m_playerController != null)
        {
            m_playerController.SelectObject();
        }
    }
}
