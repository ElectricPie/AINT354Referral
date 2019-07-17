using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 0.1f;

    //Private
    private InputHandler m_inputHandler;
    private HorizontalCameraAxisObserver m_horizontalAxisObserver;

    // Start is called before the first frame update
    void Start()
    {
        m_inputHandler = this.GetComponent<InputHandler>();

        m_horizontalAxisObserver = new HorizontalCameraAxisObserver(this);

        m_inputHandler.AddAxisObserver(m_horizontalAxisObserver, "Horizontal", 0.1f, -0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCameraHorizontally(float directionValaue)
    {
        this.transform.position += this.transform.right * directionValaue * speed;
    }
}
