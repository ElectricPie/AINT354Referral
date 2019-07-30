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
    protected BuildMenuButtons m_buildButtons;

    void Start()
    {
        m_navAgent = this.GetComponent<NavMeshAgent>();
        m_buildButtons = GameObject.Find("BuildMenu").GetComponent<BuildMenuButtons>();
    }

    public void MoveToDestination(Vector3 destination)
    {
        destination.y = this.transform.position.y;

        m_navAgent.SetDestination(destination);
    }

    public void Select()
    {
        UpdateBuildUI();
    }

    public void DeSelect()
    {

    }

    private void UpdateBuildUI()
    {
        for (int i = 0; i < m_buildButtons.buttons.Length; i++)
        {
            //Checks if the building isnt null
            if (buildingList[i] != null) {
                m_buildButtons.buttons[i].SetActive(true);
            }
            else
            {
                //Disables the button if not building is in that slot
                m_buildButtons.buttons[i].SetActive(false);
            }
        }
    }
}
