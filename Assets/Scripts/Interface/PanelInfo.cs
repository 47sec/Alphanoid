using UnityEngine;
using UnityEngine.EventSystems;

public class PanelInfo : MonoBehaviour
{
    public GameObject previousPanel;
    public GameObject initialButton;
    public GameObject backButton;

    public void SelectBackButton()
    {
        if (backButton != null)
        {
            EventSystem.current.SetSelectedGameObject(backButton);
        }
        else
        {
            Debug.Log("Back button unavailable");
        }
    }
}
