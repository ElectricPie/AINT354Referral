using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            //Disables the hotkeys and the build menu so that it no other building ghost are created
            DisableHotkeys();
            DisableUIBuildMenu();

            //Creats a new unit in the game
            GameObject newUnit = Instantiate(createables[creatableIndex]);
            //Moves the new unit to the spawn point
            newUnit.transform.position = unitSpawnPoint.transform.position;
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
        Gizmos.color = new Color(0, 1, 0, 1);
        Gizmos.DrawCube(unitSpawnPoint.transform.position, new Vector3(0.1f, 0.1f, 0.1f));
    }
}
