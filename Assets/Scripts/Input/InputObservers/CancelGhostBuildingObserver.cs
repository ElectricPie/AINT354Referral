using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelGhostBuildingObserver : InputObserver
{
    //Private
    private Unit m_unit;

    public CancelGhostBuildingObserver()
    {
        m_unit = null;
    }

    public CancelGhostBuildingObserver(Unit unit)
    {
        m_unit = unit;
    }

    public override void OnInputEvent(float value)
    {
        if (m_unit != null)
        {
            m_unit.CancelBuilding();
        }
        else
        {
            Debug.LogWarning("Cancel observer missing unit");
        }
    }
}
