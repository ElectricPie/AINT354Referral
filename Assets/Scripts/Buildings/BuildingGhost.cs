using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingGhost : MonoBehaviour
{
    //Private
    private BuildingGrid m_grid;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.layer = 2;
        //this.GetComponent<Collider>().enabled = false;
        this.GetComponent<NavMeshObstacle>().enabled = false;

        m_grid = GameObject.Find("_Grid").GetComponent<BuildingGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (m_grid != null)
            {
                //Gets the position of the nearest point from the grid
                Vector3 position = m_grid.GetNearestPoint(hit.point);
                //Fixes the y position so it dose not float
                position.y = 1;

                this.transform.position = position;
            }
            else
            {
                Debug.LogWarning("No grid found on: " + this);
            }
        }
    }

    public void PlaceBuilding()
    {
        //Renables the nav mesh obstical so units will pathfind around it
        this.GetComponent<NavMeshObstacle>().enabled = true;

        //Removes this scirpt from the building object
        Destroy(this);
    }
}
