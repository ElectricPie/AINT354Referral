using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieUnit : Unit
{
    protected override void Attack()
    {
        //Syncs the attack with the animation
        Invoke("DamageTarget", 1.2f);
    }

    void Update()
    {
        base.Update();

        if (m_target == null)
        {
            m_target = m_aiController.GetClosestEnemy(this.gameObject).GetComponent<Attackable>();
        }
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

    private void DamageTarget()
    {
        //Deals the damage to the target
        m_target.ReciveAttack(damage, this);
    }
}
