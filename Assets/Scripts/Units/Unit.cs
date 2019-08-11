using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Unit : Attackable
{
    //Protected
    protected int m_damage;
    protected NavMeshAgent m_navAgent;

    //Private
    private GameObject m_ghostBuilding;
    private CancelGhostBuildingObserver m_cancelBuildingObserver;
    private PlaceBuildingObserver m_placeBuildingObserver;

    void Start()
    {
        //Calls the base start method
        base.Start();

        m_navAgent = this.GetComponent<NavMeshAgent>();

        //Sets up other observers
        m_cancelBuildingObserver = new CancelGhostBuildingObserver(this);
        m_placeBuildingObserver = new PlaceBuildingObserver(this);
    }

    //Sets the destination of the nav mesh agent
    public void MoveToDestination(Vector3 destination)
    {
        destination.y = this.transform.position.y;

        m_navAgent.SetDestination(destination);
    }

    public void BuildBuilding(int buildingIndexValue)
    {
        if (createables[buildingIndexValue] != null)
        {
            //Disables the hotkeys and the build menu so that it no other building ghost are created
            DisableHotkeys();
            DisableUIBuildMenu();

            //Creats a new building in the game
            m_ghostBuilding = Instantiate(createables[buildingIndexValue]);
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
