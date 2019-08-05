using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingGhost : MonoBehaviour
{
    //Public
    public GameObject ghost;

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
            //this.transform.position = hit.point;
            if (m_grid != null)
            {
                this.transform.position = m_grid.GetNearestPoint(hit.point);
            }
            else
            {
                Debug.LogWarning("No grid found on: " + this);
            }
        }
    }
}
