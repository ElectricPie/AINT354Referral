using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
        for (int i = 0; i < m_buildButtons.buttons.Length; i++)
        {
            m_buildButtons.buttons[i].SetActive(false);
        }
    }

    private void UpdateBuildUI()
    {
        for (int i = 0; i < m_buildButtons.buttons.Length; i++)
        {
            //Checks if the building isnt null
            if (buildingList[i] != null) {
                m_buildButtons.buttons[i].SetActive(true);
                //Changes the buttons sprite
                m_buildButtons.buttons[i].GetComponent<Image>().sprite = buildingList[i].GetComponent<BuildingInfo>().icon;
            }
            else
            {
                //Disables the button if not building is in that slot
                m_buildButtons.buttons[i].SetActive(false);
            }
        }
    }

    public void BuildBuilding(int buildingIndexValue)
    {
        if (buildingList[buildingIndexValue] != null)
        {
            Debug.Log("Building: " + buildingList[buildingIndexValue]);
        }
        else
        {
            Debug.Log("Unit does not have a building in that slot");
        }
    }
}
