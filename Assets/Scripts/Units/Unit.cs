﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Unit : Attackable
{
    //Public
    public GameObject[] buildingList = new GameObject[12];
    public GameObject buildingGhostPrefab;

    //Protected
    protected int m_damage;
    protected NavMeshAgent m_navAgent;
    protected BuildMenuButtons m_buildButtons;
    protected KeyCode[] m_buildHotkeys = new KeyCode[12] {KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R,
                                                        KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F,
                                                        KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V};
    protected HotkeyObserver[] m_hotkeyObservers = new HotkeyObserver[12];
    protected InputHandler m_inputHandler;

    protected GameObject m_ghostBuilding;
    protected CancelGhostBuildingObserver m_cancelBuildingObserver;

    void Start()
    {
        m_navAgent = this.GetComponent<NavMeshAgent>();
        m_buildButtons = GameObject.Find("BuildMenu").GetComponent<BuildMenuButtons>();
        m_inputHandler = GameObject.Find("_PlayerController").GetComponent<InputHandler>();

        //Sets up the hotkey observers passing them this script and the building index they will represent
        for (int i = 0; i < m_hotkeyObservers.Length; i++)
        {
            m_hotkeyObservers[i] = new HotkeyObserver(this, i);
        }

        m_cancelBuildingObserver = new CancelGhostBuildingObserver(this);
    }

    public void MoveToDestination(Vector3 destination)
    {
        destination.y = this.transform.position.y;

        m_navAgent.SetDestination(destination);
    }

    public void Select()
    {
        UpdateBuildUI();
        EnableHotkeys();
    }

    public void DeSelect()
    {
        for (int i = 0; i < m_buildButtons.buttons.Length; i++)
        {
            m_buildButtons.buttons[i].SetActive(false);
        }
        DisableHotkeys();
    }

    private void UpdateBuildUI()
    {
        for (int i = 0; i < m_buildButtons.buttons.Length; i++)
        {
            //Checks if the building isnt null
            if (buildingList[i] != null)
            {
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

    private void DisableUIBuildMenu()
    {
        for (int i = 0; i < m_buildButtons.buttons.Length; i++)
        {
            m_buildButtons.buttons[i].SetActive(false);
        }
    }

    public void BuildBuilding(int buildingIndexValue)
    {
        if (buildingList[buildingIndexValue] != null)
        {
            //Disables the hotkeys and the build menu so that it no other building ghost are created
            DisableHotkeys();
            DisableUIBuildMenu();

            //Creats a new building in the game
            m_ghostBuilding = Instantiate(buildingList[buildingIndexValue]);
            m_ghostBuilding.AddComponent<BuildingGhost>();

            
            if (m_inputHandler != null)
            {
                m_inputHandler.AddKeyCodeDownObserver(m_cancelBuildingObserver, KeyCode.Escape);
            }
        }
        else
        {
            Debug.Log("Unit does not have a building in that slot");
        }
    }

    private void EnableHotkeys()
    {
        //Prevents null refernces
        if (m_inputHandler != null)
        {
            for (int i = 0; i < buildingList.Length; i++)
            {
                //Prevents hotkeys from being activated if no building is in the building slot
                if (buildingList[i] != null)
                {
                    m_inputHandler.AddKeyCodeDownObserver(m_hotkeyObservers[i], m_buildHotkeys[i]);
                }
            }
        }
    }

    private void DisableHotkeys()
    {
        //Prevents null refernces
        if (m_inputHandler != null)
        {
            for (int i = 0; i < buildingList.Length; i++)
            {
                m_inputHandler.RemoveKeyCodeDownObserver(m_hotkeyObservers[i]);
            }
        }
    }

    public void CancelBuilding()
    {
        Destroy(m_ghostBuilding);
        //Renables the hotkeys and build menu
        EnableHotkeys();
        UpdateBuildUI();

        //Removes the cancel keycode observers
        m_inputHandler.RemoveKeyCodeDownObserver(m_cancelBuildingObserver);
    }
}
