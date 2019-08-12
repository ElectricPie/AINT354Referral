using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Unit : Attackable
{
    //Public
    public float range = 2;

    //Protected
    protected int m_damage;
    protected NavMeshAgent m_navAgent;
    [SerializeField]
    protected Attackable m_target;

    //Private
    private GameObject m_ghostBuilding;
    private CancelGhostBuildingObserver m_cancelBuildingObserver;
    private PlaceBuildingObserver m_placeBuildingObserver;

    private bool m_hasTarget = false;
    private bool m_isInRangeOfTarget = false;

    void Start()
    {
        //Calls the base start method
        base.Start();

        m_navAgent = this.GetComponent<NavMeshAgent>();

        //Sets up other observers
        m_cancelBuildingObserver = new CancelGhostBuildingObserver(this);
        m_placeBuildingObserver = new PlaceBuildingObserver(this);
    }

    void Update()
    {
        if (m_hasTarget == true && Vector3.Distance(m_target.transform.position, this.transform.position) <= range)
        {
            m_navAgent.isStopped = true;
            Debug.Log(this.gameObject + " is in range to attack: " + m_target.gameObject);
        }
    }

    //Sets the destination of the nav mesh agent
    public void MoveToDestination(Vector3 destination)
    {
        destination.y = this.transform.position.y;

        m_navAgent.SetDestination(destination);

        m_navAgent.isStopped = false;
    }

    public override void BuildObject(int creatableIndex)
    {
        if (createables[creatableIndex] != null)
        {
            //Disables the hotkeys and the build menu so that it no other building ghost are created
            DisableHotkeys();
            DisableUIBuildMenu();

            //Creats a new building in the game
            m_ghostBuilding = Instantiate(createables[creatableIndex]);
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

    public void AttackTarget(Attackable target)
    {
        m_target = target;

        if (m_target != null)
        {
            m_hasTarget = true;
            m_navAgent.SetDestination(m_target.transform.position);
        }
        else
        {
            m_hasTarget = false;
        }
    }
}
