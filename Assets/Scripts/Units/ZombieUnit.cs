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

        //Gets a target if it the zombie dosent have one. 
        if (m_target == null)
        {
            //Gets the closest enemy from the ai controller
            GameObject closestEnemy = m_aiController.GetClosestEnemy(this.gameObject);

            //If there is an enemy set it as the target
            if (closestEnemy != null)
            {
                m_target = closestEnemy.GetComponent<Attackable>();
            }
            else
            {
                m_target = null;
            }
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
        //Prevents attemting to damage the target if there isnt one
        if (m_target != null)
        {
            //Deals the damage to the target
            m_target.ReciveAttack(damage, this);
        }
    }
}
