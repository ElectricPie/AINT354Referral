using System.Collections;
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

    //Private
    private BuildMenuButtons m_buildButtons;
    private KeyCode[] m_buildHotkeys = new KeyCode[12] {KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R,
                                                        KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F,
                                                        KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V};
    private HotkeyObserver[] m_hotkeyObservers = new HotkeyObserver[12];
    private InputHandler m_inputHandler;
    private GameObject m_ghostBuilding;
    private CancelGhostBuildingObserver m_cancelBuildingObserver;
    private PlaceBuildingObserver m_placeBuildingObserver;
    private PlayerController m_playerController;

    void Start()
    {
        m_navAgent = this.GetComponent<NavMeshAgent>();
        m_buildButtons = GameObject.Find("BuildMenu").GetComponent<BuildMenuButtons>();

        GameObject playerObject = GameObject.Find("_PlayerController");
        m_inputHandler = playerObject.GetComponent<InputHandler>();
        m_playerController = playerObject.GetComponent<PlayerController>();

        //Sets up the hotkey observers passing them this script and the building index they will represent
        for (int i = 0; i < m_hotkeyObservers.Length; i++)
        {
            m_hotkeyObservers[i] = new HotkeyObserver(this, i);
        }

        //Sets up other observers
        m_cancelBuildingObserver = new CancelGhostBuildingObserver(this);
        m_placeBuildingObserver = new PlaceBuildingObserver(this);
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
                m_inputHandler.AddKeyCodeDownObserver(m_placeBuildingObserver, KeyCode.Mouse0);
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
        //Destroys the ghost
        Destroy(m_ghostBuilding);

        RenableBuilding();
    }

    public void CreateBuilding()
    {
        //Resets the keybindings if placing the building was succesful
        if (m_ghostBuilding.GetComponent<BuildingGhost>().PlaceBuilding())
        {
            m_playerController.SelectedObject = this.gameObject;

            RenableBuilding();
        }
        else
        {
            Debug.Log("Cannot Place Building");
        }
    }

    private void RenableBuilding()
    {
        //Renables the hotkeys and build menu
        EnableHotkeys();
        UpdateBuildUI();

        //Removes the cancel keycode observers
        m_inputHandler.RemoveKeyCodeDownObserver(m_cancelBuildingObserver);
        m_inputHandler.RemoveKeyCodeDownObserver(m_placeBuildingObserver);
    }
}
