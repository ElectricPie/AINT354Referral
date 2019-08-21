using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    //Private
    //HashSet to prevent duplicates
    private List<GameObject> m_enemies;

    // Start is called before the first frame update
    void Start()
    {
        m_enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetClosestEnemy(GameObject unit)
    {
        //Checks if the list is empty
        if (m_enemies.Count != 0)
        {
            //Sets up vairables
            GameObject closestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;

            //Gets the current position of the unit
            Vector3 currentPosition = unit.transform.position;

            foreach (GameObject enemy in m_enemies)
            {
                Vector3 directionToTarget = enemy.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    //Sets the new closest enemy
                    closestTarget = enemy;
                }
            }

            return closestTarget;
        }

        return null;
    }

    public void AddNewEnemy(GameObject newEnemy)
    {
        Debug.Log("Enemy Count: " + m_enemies.Count);

        //Checks if the enemy is already in the list
        foreach (GameObject enemy in m_enemies)
        {
            if (enemy == newEnemy)
            {
                //Returns the method if there is a duplicate found
                return;
            }
        }

        //Adds the enemy to the list if no duplicate was found
        m_enemies.Add(newEnemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        m_enemies.Remove(enemy);
    }
}
