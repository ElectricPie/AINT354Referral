using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    //Public
    public ResourceUIBar uiBar;

    //Private
    private Resource[] m_resources = new Resource[3] { new ScrapResource(), null, null };

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Test", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI(0);
        UpdateUI(1);
        UpdateUI(2);
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
            //Prevents the resource from going above the max amout
            if ((m_resources[resourceIndex].amount + amountToBeAdded) > m_resources[resourceIndex].maxAmount)
            {
                m_resources[resourceIndex].amount = m_resources[resourceIndex].maxAmount;
            }
            else
            {
                m_resources[resourceIndex].amount += amountToBeAdded;
            }
                
            UpdateUI(resourceIndex);
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

                UpdateUI(resourceIndex);

                //Returns true if the removal was succesful
                return true;
            }
        }

        //Returns false if the amount couldnt be removed
        return false;
    }

    private void UpdateUI(int resourceIndex)
    {
        //Prevents null reference
        if (uiBar != null)
        {
            if (CheckValidResource(resourceIndex))
            {
                if (m_resources[resourceIndex] == null)
                {
                    uiBar.UpdateTab(resourceIndex, 0, 0);
                }
                else
                {
                    uiBar.UpdateTab(resourceIndex, m_resources[resourceIndex].amount, m_resources[resourceIndex].maxAmount);
                }
            }
            else
            {
                Debug.LogError("Invalid Resource, Index out of range");
            }
        }
        else
        {
            Debug.LogError("Resource UI Bar missing!");
        }
    }
}
