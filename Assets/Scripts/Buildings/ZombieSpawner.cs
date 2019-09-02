using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : Building
{
    //Public
    public float spawnTimer = 30.0f;

    //Private
    private float m_spawnTimerTracker = 0.0f;

    private void Start()
    {
        base.Start();

        GameObject newUnit = Instantiate(createables[0]);

        //Moves the new unit to the spawn point
        newUnit.GetComponent<NavMeshAgent>().Warp(unitSpawnPoint.transform.position);
    }

    void Update()
    {
        if (m_spawnTimerTracker >= spawnTimer)
        {
            GameObject newUnit = Instantiate(createables[0]);

            //Moves the new unit to the spawn point
            newUnit.GetComponent<NavMeshAgent>().Warp(unitSpawnPoint.transform.position);

            //Resets the timer
            m_spawnTimerTracker = 0.0f;

            spawnTimer -= 2.0f;
        }
        else
        {
            m_spawnTimerTracker += Time.deltaTime;
        }
    }
}
