using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeyObserver : InputObserver
{
    //Private
    private Attackable m_attackable;
    private int m_buildIndex;

    public HotkeyObserver()
    {
        m_attackable = null;
        m_buildIndex = -1;
    }

    public HotkeyObserver(Attackable attackable, int buildIndex)
    {
        m_attackable = attackable;
        m_buildIndex = buildIndex;
    }

    public override void OnInputEvent(float value)
    {
        //Prevents null reverence
        if (m_attackable != null)
        {
            if (m_attackable.GetComponent<Unit>())
            {
                m_attackable.GetComponent<Unit>().BuildBuilding(m_buildIndex);
            }
            else if (m_attackable.GetComponent<Building>())
            {

            }
        }
        else
        {
            Debug.LogWarning("No unit attached to observer");
        }
    }
}
