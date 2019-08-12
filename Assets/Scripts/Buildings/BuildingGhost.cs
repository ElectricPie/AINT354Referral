using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingGhost : MonoBehaviour
{
    //Private
    private BuildingGrid m_grid;

    private List<GameObject> m_collisionList;

    // Start is called before the first frame update
    void Start()
    {
        //Prevents the raycast from hitting the building
        this.gameObject.layer = 2;
        this.GetComponent<NavMeshObstacle>().enabled = false;

        m_grid = GameObject.Find("_Grid").GetComponent<BuildingGrid>();

        m_collisionList = new List<GameObject>();
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
                position.y = 0;

                this.transform.position = position;
            }
            else
            {
                Debug.LogWarning("No grid found on: " + this);
            }
        }
    }

    public bool PlaceBuilding()
    {
        //Places the building if nothing is colliding with it and returns if it is succesful
        if (m_collisionList.Count == 0)
        {
            //Allows raycasts to hit the building
            this.gameObject.layer = 0;

            //Renables the nav mesh obstical so units will pathfind around it
            this.GetComponent<NavMeshObstacle>().enabled = true;

            //Removes this scirpt from the building object
            Destroy(this);

            return true;
        } 
        else
        {
            return false;
        }
    } 

    void OnTriggerEnter(Collider other)
    {
        //Prevents the terrain from being a collider
        if (other.tag == "Selectable")
        {
            //Adds it to the list of currely coliding objects
            m_collisionList.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Selectable")
        {
            //Removes it to the list of currely coliding objects
            m_collisionList.Remove(other.gameObject);
        }
    }
}
