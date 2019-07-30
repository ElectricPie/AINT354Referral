using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        m_inputHandler.AddButtonObserver(m_selectObserver, "Fire1");
        m_inputHandler.AddButtonObserver(m_moveObserver, "Fire2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Selectable")
            {
                //Calls the previouslty selected object and calls its deselection method
                if (m_selectedObject != null)
                {
                    if (m_selectedObject.GetComponent<Unit>())
                    {
                        m_selectedObject.GetComponent<Unit>().DeSelect();
                    }
                }

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
}
