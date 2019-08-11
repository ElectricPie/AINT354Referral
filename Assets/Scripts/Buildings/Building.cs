using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Attackable
{
    public GameObject unitSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateUnit(int unitIndexValue)
    {

    }

    protected override void Die()
    {
        Destroy(this);
    }

}
