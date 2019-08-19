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
        //Lines the muzzle flash with the animation
        Invoke("EnableFlash", 0.8f);
    }

    protected override void Die()
    {
        m_animator.SetBool("isAlive", false);
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
