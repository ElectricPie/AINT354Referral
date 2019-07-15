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

        TestObserver axisTest = new TestObserver();

        m_inputHandler.AddKeyCodeObserver(new TestObserver(), KeyCode.W);
        m_inputHandler.AddButtonObserver(new TestObserver(), "Fire1");
        m_inputHandler.AddAxisObserver(axisTest, "Horizontal", 2.0f, 2.0f);

        m_inputHandler.RemoveAxisObserver(axisTest);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
