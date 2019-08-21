﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public abstract class Unit : Attackable
{
    //Public
    public float range = 2;
    public int damage = 0;
    public float attackSpeed;

    //Protected
    protected NavMeshAgent m_navAgent;
    [SerializeField]
    protected Attackable m_target;
    protected Animator m_animator;
    [SerializeField]
    protected float m_attackTimer = 0.0f;
    protected AIController m_aiController;

    //Private
    private GameObject m_ghostBuilding;
    private CancelGhostBuildingObserver m_cancelBuildingObserver;
    private PlaceBuildingObserver m_placeBuildingObserver;
    private ResourceCounter m_resourceCounter;

    protected void Start()
    {
        //Calls the base start method
        base.Start();

        m_navAgent = this.GetComponent<NavMeshAgent>();

        //Sets up other observers
        m_cancelBuildingObserver = new CancelGhostBuildingObserver(this);
        m_placeBuildingObserver = new PlaceBuildingObserver(this);

        m_animator = this.GetComponent<Animator>();

        if (m_animator != null)
        {
            //Sets the attack speed for the attacking animation
            m_animator.SetFloat("attackSpeed", 1 / attackSpeed);
        }

        //Finds the AI controller
        m_aiController = GameObject.Find("_AIController").GetComponent<AIController>();

        //Sends a message if the ai controller cannot be found
        if (m_aiController == null)
        {
            Debug.LogWarning("AI Controller not found!");
        }

        m_resourceCounter = m_playerController.GetComponent<ResourceCounter>();
    }

    protected void Update()
    {
        //Attacks the target if in range
        if (m_target != null && Vector3.Distance(m_target.transform.position, this.transform.position) <= range)
        {
            m_navAgent.isStopped = true;

            m_animator.SetBool("isAttacking", true);

            //Attacks the target every (attack speed) amount of time
            if (m_attackTimer >= attackSpeed)
            {
                //Resets the attack timer after the rifleman has fired
                m_attackTimer = 0.0f;
                Attack();
            }
            else
            {
                m_attackTimer += Time.deltaTime;
            }
        }
        //Move to the target if there is one
        else if(m_target != null && Vector3.Distance(m_target.transform.position, this.transform.position) >= range)
        {
            AttackTarget(m_target);
            m_animator.SetBool("isAttacking", false);
        }
        //Do nothing
        else
        {
            m_animator.SetBool("isAttacking", false);
        }
    }

    //Sets the destination of the nav mesh agent
    public void MoveToDestination(Vector3 destination)
    {
        destination.y = this.transform.position.y;
        destination.y = this.transform.position.y;

        m_navAgent.SetDestination(destination);

        m_navAgent.isStopped = false;

        m_animator.SetBool("isAttacking", false);
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
        Attackable buildingInfo = m_ghostBuilding.GetComponent<Attackable>();

        //Checks if there are enough resources to create the building
        if (m_resourceCounter.ReduceResourceAmount(buildingInfo.resourceTypeRequired, buildingInfo.resourceCost))
        {
            //Resets the keybindings if placing the building was succesful
            if (m_ghostBuilding.GetComponent<BuildingGhost>().PlaceBuilding())
            {
                m_playerController.SelectedObject = this.gameObject;

                RenableBuilding();
            }
            Debug.Log("Cannot Place Building: Other Object Blocking Placement");
        }

        Debug.Log("Cannot Place Building: Insufficent Resource");
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
            m_navAgent.isStopped = false;
            m_navAgent.SetDestination(m_target.transform.position);
        }
    }

    public void StopAttacking()
    {
        m_target = null;
    }

    protected abstract void Attack();
}
