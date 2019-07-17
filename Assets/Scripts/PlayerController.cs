using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Unit testUnit;

    private static InputHandler m_inputHandler;

    // Start is called before the first frame update
    void Start()
    {
        m_inputHandler = this.GetComponent<InputHandler>();

        InvokeRepeating("TestDamage", 0, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TestDamage()
    {
        if (testUnit != null)
        testUnit.ReciveAttack(1);
    }
}
