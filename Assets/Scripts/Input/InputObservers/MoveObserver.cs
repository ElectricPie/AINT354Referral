using UnityEngine;
using System.Collections;

public class MoveObserver : InputObserver
{
    //Private
    private PlayerController m_playerController;

    public MoveObserver()
    {
        m_playerController = null;
    }

    public MoveObserver(PlayerController playerController)
    {
        m_playerController = playerController;
    }

    public override void OnInputEvent(float value)
    {
        m_playerController.MoveSelected();
    }
}
