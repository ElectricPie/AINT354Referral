using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Attackable
{
    public GameObject[] createableUnits = new GameObject[12];

    public GameObject unitSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateUnit(int unitIndexValue)
    {
        if (createableUnits.Length > 0 && createableUnits.Length > unitIndexValue && createableUnits[unitIndexValue] != null)
        { 
            Instantiate(createableUnits[unitIndexValue]);
        }
    }

    protected override void Die()
    {
        Destroy(this);
    }

}
