using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    //Private
    private List<EventData> m_observeredAxis = new List<EventData>();
    private List<EventData> m_observeredButtons = new List<EventData>();
    private List<EventData> m_observeredKeys = new List<EventData>();

    // Update is called once per frame
    void Update()
    {
        //Axis
        for (int i = 0; i < m_observeredAxis.Count; i++)
        {
            float axisValue = Input.GetAxis(m_observeredAxis[i].axis);

            if (axisValue > m_observeredAxis[i].positiveTriggerValue || axisValue < m_observeredAxis[i].negativeTriggerValue)
            {
                m_observeredAxis[i].observer.OnInputEvent(Input.GetRawAxis(m_observeredAxis[i].axis));
            }
        }

        //Buttons
        for (int i = 0; i < m_observeredButtons.Count; i++)
        {
            if (Input.GetButton(m_observeredButtons[i].button))
            {
                m_observeredButtons[i].observer.OnInputEvent(1.0f);
            }
        }

        //Keys
        for (int i = 0; i < m_observeredKeys.Count; i++)
        {
            if (Input.GetKey(m_observeredKeys[i].keyCode))
            {
                m_observeredKeys[i].observer.OnInputEvent(1.0f);
            }
        }
    }

    /// <summary>
    /// Adds a new observer using an axis/
    /// </summary>
    /// <param name="newObserver">The observer to be added</param>
    /// <param name="axisName">The name button that will cause the notification</param>
    /// <param name="positiveTrigger">The postive value that will trigger the notification (values cannot be lower that 0.1)</param>
    /// <param name="negativeTrigger">The negative value that will trigger the notification (values cannot be highter that -0.1)</param>
    public void AddAxisObserver(InputObserver newObserver, string axisName, float positiveTrigger, float negativeTrigger)
    {
        EventData newEvent = new EventData();
        newEvent.axis = axisName;

        ///Constant notification  prevention
        //Prevents values lower that 0.1 for the positive trigger
        if (positiveTrigger <= 0)
        {
            positiveTrigger = 0.1f;
        }
        //Prevents values greater that -0.1 for the negative trigger
        if (negativeTrigger >= 0)
        {
            negativeTrigger = -0.1f;
        }

        newEvent.positiveTriggerValue = positiveTrigger;
        newEvent.negativeTriggerValue = negativeTrigger;
        newEvent.observer = newObserver;

        m_observeredAxis.Add(newEvent);
    }

    /// <summary>
    /// Adds a new observer using buttons
    /// </summary>
    /// <param name="newObserver">The observer to be added</param>
    /// <param name="keyCode">The name button that will cause the notification</param>
    public void AddButtonObserver(InputObserver newObserver, string buttonName)
    {
        EventData newEvent = new EventData();
        newEvent.button = buttonName;
        newEvent.observer = newObserver;

        m_observeredButtons.Add(newEvent);
    }

    /// <summary>
    /// Adds a new observer using keycodes
    /// </summary>
    /// <param name="newObserver">The observer to be added</param>
    /// <param name="keyCode">The keycode that will cause the notification</param>
    public void AddKeyCodeObserver(InputObserver newObserver, KeyCode keyCode)
    {
        EventData newEvent = new EventData();
        newEvent.keyCode = keyCode;
        newEvent.observer = newObserver;

        m_observeredKeys.Add(newEvent);
    }

    /// <summary>
    /// Removes the given input observer if it exists in the axis observers
    /// </summary>
    /// <param name="observerToBeRemoved">The observer that will be removed</param>
    public void RemoveAxisObserver(InputObserver observerToBeRemoved)
    {
        for (int i = 0; i < m_observeredAxis.Count; i++)
        {
            //Checks if the given observer is in the list
            if (m_observeredAxis[i].observer == observerToBeRemoved)
            {
                m_observeredAxis.RemoveAt(i);
                return;
            }
        }
    }

    /// <summary>
    /// Removes the given input observer if it exists in the button observers
    /// </summary>
    /// <param name="observerToBeRemoved">The observer that will be removed</param>
    public void RemoveButtonObserver(InputObserver observerToBeRemoved)
    {
        for (int i = 0; i < m_observeredButtons.Count; i++)
        {
            //Checks if the given observer is in the list
            if (m_observeredButtons[i].observer == observerToBeRemoved)
            {
                m_observeredButtons.RemoveAt(i);
                return;
            }
        }
    }

    /// <summary>
    /// Removes the given input observer if it exists in the keycode observers
    /// </summary>
    /// <param name="observerToBeRemoved">The observer that will be removed</param>
    public void RemoveKeyCodeObserver(InputObserver observerToBeRemoved)
    {
        for (int i = 0; i < m_observeredKeys.Count; i++)
        {
            //Checks if the given observer is in the list
            if (m_observeredKeys[i].observer == observerToBeRemoved)
            {
                m_observeredKeys.RemoveAt(i);
                return;
            }
        }
    }

    /// <summary>
    /// Removes the observer from all observation lists
    /// </summary>
    /// <param name="observerToBeRemoved">The observer that will be removed</param>
    public void RemoveFromAllObsevers(InputObserver observerToBeRemoved)
    {
        RemoveAxisObserver(observerToBeRemoved);
        RemoveButtonObserver(observerToBeRemoved);
        RemoveKeyCodeObserver(observerToBeRemoved);
    }
}

public class EventData
{
    public string axis = null;
    public float positiveTriggerValue = 1;
    public float negativeTriggerValue = 1;
    public string button = null;
    public KeyCode keyCode = KeyCode.None;

    public InputObserver observer;
}