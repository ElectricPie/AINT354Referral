using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingGhost : MonoBehaviour
{
    //Public
    public GameObject ghost;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.layer = 2;
        //this.GetComponent<Collider>().enabled = false;
        this.GetComponent<NavMeshObstacle>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            this.transform.position = hit.point;
        }
    }
}
