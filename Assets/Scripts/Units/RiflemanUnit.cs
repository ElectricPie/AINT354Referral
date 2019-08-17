using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiflemanUnit : Unit
{
    public ParticleSystem muzzleFlash;
    private GameObject m_muzzleFlashLight;

    void Start()
    {
        base.Start();

        m_muzzleFlashLight = muzzleFlash.gameObject.transform.GetChild(1).gameObject;
        DisableFlash();
    }

    protected override void Attack()
    {
        m_animator.SetBool("isAttacking", true);

        //Attacks the target every (attack speed) amount of time
        if (m_attackTimer >= attackSpeed)
        {
            //Resets the attack timer after the rifleman has fired
            m_attackTimer = 0.0f;
            //Deals the damage to the target
            m_target.ReciveAttack(damage, this);

            //Lines the muzzle flash with the animation
            Invoke("EnableFlash", 0.8f);
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

    private void EnableFlash()
    {
        muzzleFlash.Play(true);
        m_muzzleFlashLight.SetActive(true);
        //Disables the muzzle flash after a set time
        Invoke("DisableFlash", 0.5f);
    }

    private void DisableFlash()
    {
        muzzleFlash.Stop(true);
        m_muzzleFlashLight.SetActive(false);
    }
}
