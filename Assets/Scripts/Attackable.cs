using UnityEngine;
using System.Collections;

public abstract class Attackable : MonoBehaviour
{
    //Public
    public int baseHealth;
    public int m_healthPerUpgrade;

    //Protected
    protected string m_name;
    protected string m_discription;
    protected int m_maxHealth;
    protected int m_currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        m_maxHealth = baseHealth;
        m_currentHealth = baseHealth;
    }

    public void ReciveAttack(int damage)
    {
        m_currentHealth -= damage;

        //Checks if the attackables health is below 0 and if it is calls its death code
        if (m_currentHealth <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
