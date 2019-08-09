using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuildingObserver : InputObserver
{
    private Unit m_unit;

    public PlaceBuildingObserver()
    {
        m_unit = null;
    }

    public PlaceBuildingObserver(Unit unit)
    {
        m_unit = unit;
    }

    public override void OnInputEvent(float value)
    {
        if (m_unit != null)
        {
            m_unit.CreateBuilding();
        }
        else
        {
            Debug.Log(this + " is missing unit reference");
        }
    }
}
