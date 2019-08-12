using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private static InputHandler m_inputHandler;

    private SelectObserver m_selectObserver;
    private MoveObserver m_moveObserver;

    [SerializeField]
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
                Debug.Log("Selecting raycast: " + hit.transform.gameObject);
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
                    if (m_selectedObject.GetComponent<Attackable>())
                    {
                        m_selectedObject.GetComponent<Attackable>().Select();
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
                if (hit.transform.gameObject.GetComponent<Attackable>())
                {
                    //Sets the units target and calls it to attack the target
                    m_selectedObject.GetComponent<Unit>().AttackTarget(hit.transform.gameObject.GetComponent<Attackable>());
                }
                else
                {
                    //Removes the units target and sets its destination
                    m_selectedObject.GetComponent<Unit>().AttackTarget(null);
                    m_selectedObject.GetComponent<Unit>().MoveToDestination(hit.point);
                }
            }
        }
    }

    public void BuildFromSelectedUnit(int objectIndexValue)
    {
        //Error prevention
        if(m_selectedObject != null)
        {
            //Makes sure the object is a unit
            if (m_selectedObject.GetComponent<Attackable>())
            {
                //Calls the method for creating a building from the selected unit
                m_selectedObject.GetComponent<Attackable>().BuildObject(objectIndexValue);
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
