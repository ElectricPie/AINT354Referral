using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Private
    private InputHandler m_inputHandler;
    private HorizontalCameraAxisObserver m_horizontalAxisObserver;

    // Start is called before the first frame update
    void Start()
    {
        m_inputHandler = this.GetComponent<InputHandler>();

        m_horizontalAxisObserver = new HorizontalCameraAxisObserver(this.gameObject);

        m_inputHandler.AddAxisObserver(m_horizontalAxisObserver, "Horizontal", 0.1f, -0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
