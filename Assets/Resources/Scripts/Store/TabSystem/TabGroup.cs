using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;

    private Color tabIdle = new Color(80, 110, 140);
    private Color tabHover = new Color(65, 91, 116);
    private Color tabActive = new Color(39, 174, 96); 
    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>(); 
        }
        
        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        button.background.color = tabHover; 
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        ResetTabs();
        button.background.color = tabActive; 
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            button.background.color = tabIdle; 
        }
    }
}
