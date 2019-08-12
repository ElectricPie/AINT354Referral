using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class Attackable : MonoBehaviour
{
    //Public
    public int baseHealth;
    public int m_healthPerUpgrade;

    public string description = "";
    //public Resoruce resourceTypeRequired;
    public int resourceCost = 0;
    public Sprite icon = null;

    public GameObject[] createables = new GameObject[12];

    //Protected
    protected string m_name;
    protected string m_discription;
    protected int m_maxHealth;
    [SerializeField]
    protected int m_currentHealth;

    //Private
    protected BuildMenuButtons m_buildButtons;
    protected KeyCode[] m_buildHotkeys = new KeyCode[12] {KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R,
                                                        KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F,
                                                        KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V};
    protected HotkeyObserver[] m_hotkeyObservers = new HotkeyObserver[12];
    protected InputHandler m_inputHandler;
    protected PlayerController m_playerController;

    // Start is called before the first frame update
    protected void Start()
    {
        m_maxHealth = baseHealth;
        m_currentHealth = baseHealth;

        m_buildButtons = GameObject.Find("BuildMenu").GetComponent<BuildMenuButtons>();

        GameObject playerObject = GameObject.Find("_PlayerController");
        m_inputHandler = playerObject.GetComponent<InputHandler>();
        m_playerController = playerObject.GetComponent<PlayerController>();

        //Sets up the hotkey observers passing them this script and the building index they will represent
        for (int i = 0; i < m_hotkeyObservers.Length; i++)
        {
            m_hotkeyObservers[i] = new HotkeyObserver(this, i);
        }
    }

    public void ReciveAttack(int damage)
    {
        Debug.Log("Recived Hit: " + damage);

        m_currentHealth -= damage;

        //Checks if the attackables health is below 0 and if it is calls its death code
        if (m_currentHealth <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();

    //Enables the build menu and hotkeys
    public void Select()
    {
        UpdateBuildUI();
        EnableHotkeys();
    }

    //Removes the UI build menu and disables the hot keys
    public void DeSelect()
    {
        for (int i = 0; i < m_buildButtons.buttons.Length; i++)
        {
            m_buildButtons.buttons[i].SetActive(false);
        }
        DisableHotkeys();
    }

    //Updates the UI build menu to show what attackables can be created from this attackable
    protected void UpdateBuildUI()
    {
        for (int i = 0; i < m_buildButtons.buttons.Length; i++)
        {
            //Checks if the building isnt null
            if (createables[i] != null)
            {
                if (createables[i].GetComponent<Attackable>())
                {
                    m_buildButtons.buttons[i].SetActive(true);
                    //Changes the buttons sprite
                    m_buildButtons.buttons[i].GetComponent<Image>().sprite = createables[i].GetComponent<Attackable>().icon;
                }
            }
            else
            {
                //Disables the button if not building is in that slot
                m_buildButtons.buttons[i].SetActive(false);
            }
        }
    }

    //Disables the all buttons in the build menu ui
    protected void DisableUIBuildMenu()
    {
        for (int i = 0; i < m_buildButtons.buttons.Length; i++)
        {
            m_buildButtons.buttons[i].SetActive(false);
        }
    }

    //Enables the building hot keys if a attackable is in the list
    protected void EnableHotkeys()
    {
        //Prevents null refernces
        if (m_inputHandler != null)
        {
            for (int i = 0; i < createables.Length; i++)
            {
                //Prevents hotkeys from being activated if no building is in the building slot
                if (createables[i] != null)
                {
                    m_inputHandler.AddKeyCodeDownObserver(m_hotkeyObservers[i], m_buildHotkeys[i]);
                }
            }
        }
    }

    //Disables all hotkeys
    protected void DisableHotkeys()
    {
        //Prevents null refernces
        if (m_inputHandler != null)
        {
            for (int i = 0; i < createables.Length; i++)
            {
                m_inputHandler.RemoveKeyCodeDownObserver(m_hotkeyObservers[i]);
            }
        }
    }

    public abstract void BuildObject(int creatableIndex);
}
