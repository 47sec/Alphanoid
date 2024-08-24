using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManagerV2 : MonoBehaviour
{
    public GameObject activePanel;
    //
    private GameObject lastSelectedButton;
    // Режим ручного перемещения
    public bool navigationMode;



    void OnEnable()
    {
        lastSelectedButton = activePanel.GetComponent<PanelInfo>().initialButton;
        if (navigationMode)
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedButton);
        }



        EventTrigger trigger = GetComponent<EventTrigger>();

        EventTrigger.Entry entryDeselect = new EventTrigger.Entry();
        entryDeselect.eventID = EventTriggerType.PointerDown;
        entryDeselect.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        trigger.triggers.Add(entryDeselect);
    }
    public void OnPointerDownDelegate(PointerEventData data)
    {
        navigationMode = false;
        Debug.Log("Deselected");
    }
    public void Update()
    {
        if(navigationMode == false && (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")))
        {
            navigationMode = true;
            EventSystem.current.SetSelectedGameObject(lastSelectedButton);
        }
    }
    public void MoveToPreviousPanel()
    {
        PanelInfo pf = activePanel.GetComponent<PanelInfo>();

        if(pf.previousPanel != null)
        {
            //
            activePanel.SetActive(false);
            activePanel = pf.previousPanel;
            activePanel.SetActive(true);

            //
            lastSelectedButton = activePanel.GetComponent<PanelInfo>().initialButton;
            if(navigationMode)
            {
                EventSystem.current.SetSelectedGameObject(lastSelectedButton);
            }

            Debug.Log("Returned to " + activePanel.name);
        }
        else
        {
            Debug.Log("Previous panel unavailable");
        }
    }
    public void MoveToPanel(GameObject panel)
    {
        // save button pos
        activePanel.GetComponent<PanelInfo>().initialButton = lastSelectedButton;

        //
        activePanel.SetActive(false);
        activePanel = panel;
        activePanel.SetActive(true);

        //
        lastSelectedButton = activePanel.GetComponent<PanelInfo>().initialButton;
        if(navigationMode)
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedButton);
        }

        Debug.Log("Moved to " + activePanel.name);
    }
    public void UpdateLastSelectedButton()
    {
        lastSelectedButton = EventSystem.current.currentSelectedGameObject;
    }
}