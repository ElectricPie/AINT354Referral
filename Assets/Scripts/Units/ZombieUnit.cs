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
        if (m_animator != null)
        {
            m_animator.SetTrigger("isAlive");
        }

        //Prevents the unit from being selected
        this.GetComponent<Collider>().enabled = false;

        Invoke("Destroy", 4);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
