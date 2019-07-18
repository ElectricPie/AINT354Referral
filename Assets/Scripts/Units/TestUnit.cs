using UnityEngine;
using System.Collections;

public class TestUnit : Unit
{
    public TestUnit() : base()
    {

    }

    protected override void Die()
    {
        Destroy(this.gameObject);
    }
}
