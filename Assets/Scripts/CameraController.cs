using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camera;

    public float speed = 0.1f;
    public float zoomSpeed = 1.0f;

    [Range(4, 0)]
    public float cameraZoomMaxLimit = 4.0f;
    [Range(-4, 0)]
    public float cameraZoomMinLimit = -4.0f;

    public bool mouseScrollingIsEnabled = true;

    //Private
    private InputHandler m_inputHandler;
    private HorizontalCameraAxisObserver m_horizontalAxisObserver;
    private VerticalCameraAxisObserver m_verticalAxisObserver;
    private ZoomCameraAxisObserver m_zoomAxisObserver;

    // Start is called before the first frame update
    void Start()
    {
        m_inputHandler = this.GetComponent<InputHandler>();

        SetUpObservers();
    }

    // Update is called once per frame
    void Update()
    {
        MouseMovement();
    }

    private void SetUpObservers()
    {
        //Creates the observers
        m_horizontalAxisObserver = new HorizontalCameraAxisObserver(this);
        m_verticalAxisObserver = new VerticalCameraAxisObserver(this);
        m_zoomAxisObserver = new ZoomCameraAxisObserver(this);

        //Sets the observers to listen for their input
        m_inputHandler.AddAxisObserver(m_horizontalAxisObserver, "Horizontal", 0.1f, -0.1f);
        m_inputHandler.AddAxisObserver(m_verticalAxisObserver, "Vertical", 0.1f, -0.1f);
        m_inputHandler.AddAxisObserver(m_zoomAxisObserver, "Mouse ScrollWheel", 0.01f, -0.01f);
    }

    public void MouseMovement()
    {
        if (mouseScrollingIsEnabled)
        {
            //Vertical mouse movement
            if (Input.mousePosition.y >= Screen.height * 0.95)
            {
                MoveCameraVertically(1);
            }
            else if (Input.mousePosition.y <= Screen.height * 0.05)
            {
                MoveCameraVertically(-1);
            }

            //Horizontal mouse movement
            if (Input.mousePosition.x >= Screen.width * 0.95)
            {
                MoveCameraHorizontally(1);
            }
            else if (Input.mousePosition.x <= Screen.width * 0.05)
            {
                MoveCameraHorizontally(-1);
            }
        }
    }

    public void MoveCameraHorizontally(float directionValaue)
    {
        this.transform.position += this.transform.right * directionValaue * speed;
    }

    public void MoveCameraVertically(float directionValaue)
    {
        this.transform.position += this.transform.forward * directionValaue * speed;
    }

    public void ZoomCamera(float directionValue)
    {
        //Prevents the camera from going past its limits
        if ((directionValue > 0 && camera.transform.localPosition.y < cameraZoomMinLimit) || (directionValue < 0 && camera.transform.localPosition.y > cameraZoomMaxLimit))
            return;

        camera.transform.localPosition += camera.transform.forward * directionValue * zoomSpeed;
    }
}
