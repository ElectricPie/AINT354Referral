using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeyObserver : InputObserver
{
    //Private
    private Unit m_unit;
    private int m_buildIndex;

    public HotkeyObserver()
    {
        m_unit = null;
        m_buildIndex = -1;
    }

    public HotkeyObserver(Unit unit, int buildIndex)
    {
        m_unit = unit;
        m_buildIndex = buildIndex;
    }

    public override void OnInputEvent(float value)
    {
        //Prevents null reverence
        if (m_unit != null)
        {
            m_unit.BuildBuilding(m_buildIndex);
        }
        else
        {
            Debug.LogWarning("No unit attached to observer");
        }
    }
}
