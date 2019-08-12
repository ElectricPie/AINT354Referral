using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiflemanUnit : Unit
{
    protected override void Attack()
    {
        m_animator.SetBool("isAttacking", true);

        //Attacks the target every (attack speed) amount of time
        if (m_attackTimer >= attackSpeed)
        {
            m_attackTimer = 0.0f;
            m_target.ReciveAttack(damage, this);
        }
        else
        {
            m_attackTimer += Time.deltaTime;
        }
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }
}
