using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Building : Attackable
{
    public GameObject unitSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Die()
    {
        Destroy(this);
    }

    public override void BuildObject(int creatableIndex)
    {
        
        if (createables[creatableIndex] != null)
        {
            Attackable unitInfo = createables[creatableIndex].GetComponent<Attackable>();

            if (m_resourceCounter.ReduceResourceAmount(unitInfo.resourceTypeRequired, unitInfo.resourceCost))
            {
                //Disables the hotkeys and the build menu so that it no other building ghost are created
                DisableHotkeys();
                DisableUIBuildMenu();

                //Creats a new unit in the game
                GameObject newUnit = Instantiate(createables[creatableIndex]);

                Debug.Log("Spawnpoint: " + unitSpawnPoint.transform.position);
                //Moves the new unit to the spawn point
                //newUnit.transform.position = unitSpawnPoint.transform.position;
                newUnit.GetComponent<NavMeshAgent>().Warp(unitSpawnPoint.transform.position);
            }
            else
            {
                Debug.Log("Insufficient resources to create unit");
            }
        }
        else
        {
            Debug.Log("Building does not have a unit in that slot");
        }

        m_playerController.SelectedObject = this.gameObject;

        EnableHotkeys();
        UpdateBuildUI();
    }

    //Draws a gizmo to show the unit spawn point
    void OnDrawGizmos()
    {
        if (unitSpawnPoint != null)
        {
            Gizmos.color = new Color(0, 1, 0, 1);
            Gizmos.DrawCube(unitSpawnPoint.transform.position, new Vector3(0.1f, 0.1f, 0.1f));
        }
    }
}
