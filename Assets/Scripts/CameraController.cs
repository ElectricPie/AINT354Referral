﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 0.1f;

    //Private
    private InputHandler m_inputHandler;
    private HorizontalCameraAxisObserver m_horizontalAxisObserver;
    private VerticalCameraAxisObserver m_verticalAxisObserver;

    // Start is called before the first frame update
    void Start()
    {
        m_inputHandler = this.GetComponent<InputHandler>();

        SetUpObservers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetUpObservers()
    {
        //Creates the observers
        m_horizontalAxisObserver = new HorizontalCameraAxisObserver(this);
        m_verticalAxisObserver = new VerticalCameraAxisObserver(this);

        //Sets the observers to listen for their input
        m_inputHandler.AddAxisObserver(m_horizontalAxisObserver, "Horizontal", 0.1f, -0.1f);
        m_inputHandler.AddAxisObserver(m_verticalAxisObserver, "Vertical", 0.1f, -0.1f);
    }

    public void MoveCameraHorizontally(float directionValaue)
    {
        this.transform.position += this.transform.right * directionValaue * speed;
    }

    public void MoveCameraVertically(float directionValaue)
    {
        this.transform.position += this.transform.forward * directionValaue * speed;
    }
}
