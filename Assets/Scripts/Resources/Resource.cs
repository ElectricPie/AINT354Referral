using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource
{
    //Public
    public int amount = 0;
    public int maxAmount = 100;

    //Private
    private string m_name;

    public Resource()
    {
        m_name = this.GetType().Name;
    }

    public string Name
    {
        get { return m_name; }
    }
}
