using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    [Tooltip("�������")]
    public Canvas canvas;

    [Tooltip("��������� ����� ��������� ������")]
    public string prefix;

    [Tooltip("�������� ������� ������")]
    private string currentPanelName;

    [Tooltip("������� �������")]
    public Dictionary<string, GameObject> panels = new Dictionary<string, GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            GameObject tmp = canvas.transform.GetChild(i).gameObject;
            panels.Add(tmp.name, tmp);

            if (tmp.activeSelf)
            {
                currentPanelName = tmp.name;
            }

            Debug.Log("FOUND " + tmp.name);
        }
    }

    public void ChangeCurrentPanel(string layerName)
    {
        panels[currentPanelName].SetActive(false);
        currentPanelName = prefix + layerName;
        panels[currentPanelName].SetActive(true);
    }
}
