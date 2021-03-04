using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{

    private List<TabButton> tabButtons;
    [SerializeField]
    private Color tabIdle;
    [SerializeField]
    private Color tabActive;
    [SerializeField]
    private TabButton selectedTab;
    [SerializeField]
    private List<GameObject> objectsToSwap;

    private void Start()
    {
        objectsToSwap[0].SetActive(true);
    }

    public void Subscribe(TabButton tabButton)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(tabButton);
    }

    public void OnTabExit(TabButton tabButton)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton tabButton)
    {
        selectedTab = tabButton;
        ResetTabs();
        tabButton.background.color = tabActive;
        int index = tabButton.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    private void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab) { continue; }
            button.background.color = tabIdle;
        }
    }
}
