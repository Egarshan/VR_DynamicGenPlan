using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public GameObject currentPanel, nextPanel;
    public Button switchBtn;

    private void Start()
    {
        switchBtn.onClick.AddListener(Switch);
    }

    protected void Switch()
    {
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
    }
}
