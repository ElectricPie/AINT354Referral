using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieUnit : Unit
{
    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void Die()
    {
        Destroy(this.gameObject);
    }
}
