using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : Attackable
{
    //Public
    public GameObject[] buildingList = new GameObject[12];

    //Protected
    protected int m_damage;
    protected NavMeshAgent m_navAgent;

    void Start()
    {
        m_navAgent = this.GetComponent<NavMeshAgent>();
        //m_navAgent.SetDestination(this.transform.position);
    }

    public void MoveToDestination(Vector3 destination)
    {
        destination.y = this.transform.position.y;

        m_navAgent.SetDestination(destination);
    }
}
