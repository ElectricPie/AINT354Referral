using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    private Resource[] m_resources = new Resource[] { new ScrapResource() };

    // Start is called before the first frame update
    void Start()
    {
        IncreaseResourceAmount(1, 3);

        IncreaseResourceAmount(-1, 3);

        InvokeRepeating("Test", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Scrap Amount: " + m_resources[0].amount);
    }

    private void Test()
    {
        IncreaseResourceAmount(0, 3);
        ReduceResourceAmount(0, 1);
    }

    private bool CheckValidResource(int resourceIndex)
    {
        if (resourceIndex >= 0 && resourceIndex < m_resources.Length)
        {
            return true;
        }

        Debug.LogError("Invalid Resource, Index out of range");
        return false;
    }

    public void IncreaseResourceAmount(int resourceIndex, int amountToBeAdded)
    {
        //Prevents attempts at adding to a nonexistant resources
        if (CheckValidResource(resourceIndex))
        {
            m_resources[resourceIndex].amount += amountToBeAdded;
        }
    }

    public bool ReduceResourceAmount(int resourceIndex, int amountToBeRemoved)
    {
        if (CheckValidResource(resourceIndex))
        {
            //Checks if the amount to be remove will bring it under 0
            if ((m_resources[resourceIndex].amount - amountToBeRemoved) >= 0)
            {
                m_resources[resourceIndex].amount -= amountToBeRemoved;

                //Returns true if the removal was succesful
                return true;
            }
        }

        //Returns false if the amount couldnt be removed
        return false;
    }
}
