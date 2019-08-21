using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUIBar : MonoBehaviour
{
    public GameObject[] resourceTabs = new GameObject[3];

    public void UpdateTab(int tabIndex, int newValue, int maxValue)
    {
        if (tabIndex >= 0 && tabIndex < resourceTabs.Length)
        {
            Text uiText = resourceTabs[tabIndex].transform.GetChild(1).GetComponent<Text>();

            string tabString = newValue + "/" + maxValue;

            uiText.text = tabString;
        }
    }
}
