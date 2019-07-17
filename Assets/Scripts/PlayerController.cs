using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static InputHandler m_inputHandler;

    // Start is called before the first frame update
    void Start()
    {
        m_inputHandler = this.GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
