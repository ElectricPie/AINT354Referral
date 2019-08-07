using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private static InputHandler m_inputHandler;

    private SelectObserver m_selectObserver;
    private MoveObserver m_moveObserver;

    private GameObject m_selectedObject;

    // Start is called before the first frame update
    void Start()
    {
        m_inputHandler = this.GetComponent<InputHandler>();

        m_selectObserver = new SelectObserver(this);
        m_moveObserver = new MoveObserver(this);

        m_inputHandler.AddButtonObserverDown(m_selectObserver, "Fire1");
        m_inputHandler.AddButtonObserverDown(m_moveObserver, "Fire2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectObject()
    {
        //Prevents raycasting if mouse is over the UI
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                //Calls the previouslty selected object and calls its deselection method
                if (m_selectedObject != null)
                {
                    if (m_selectedObject.GetComponent<Unit>())
                    {
                        m_selectedObject.GetComponent<Unit>().DeSelect();
                    }
                }

                //Selects the new object if it is selectable
                if (hit.transform.tag == "Selectable")
                {

                    //Selects the new object
                    m_selectedObject = hit.transform.gameObject;

                    //Calls the selected objects selection method
                    if (m_selectedObject.GetComponent<Unit>())
                    {
                        m_selectedObject.GetComponent<Unit>().Select();
                    }
                }
                else
                {
                    m_selectedObject = null;
                }
            }
        }
    }

    public void MoveSelected()
    {
        if (m_selectedObject != null && m_selectedObject.GetComponent<Unit>())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                m_selectedObject.GetComponent<Unit>().MoveToDestination(hit.point);
            }
        }
    }

    public void BuildFromSelectedUnit(int buildingIndexValue)
    {
        //Error prevention
        if(m_selectedObject != null)
        {
            //Makes sure the object is a unit
            if (m_selectedObject.GetComponent<Unit>())
            {
                //Calls the method for creating a building from the selected unit
                m_selectedObject.GetComponent<Unit>().BuildBuilding(buildingIndexValue);
            }
        }
       
    }

    public GameObject SelectedObject
    {
        set
        {
            if (value.GetComponent<Attackable>())
            {
                m_selectedObject = value;
            }
        }
    }
}
