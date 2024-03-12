using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tabs : MonoBehaviour
{
    public List<Tab_Button> tabButtons;
    public List<GameObject> objectToSwap;
    public Sprite enterSprite, selectedSprite, exitSprite;
    private Tab_Button selectedTab;
    public void Subscribe(Tab_Button button){
        tabButtons ??= new List<Tab_Button>();
        tabButtons.Add(button);
    }

    public void OnTabEnter(Tab_Button button){
        ResetTabs();
        if(selectedTab == null || button != selectedTab){
            button.background.sprite = enterSprite;
        }
    }

    private void ResetTabs(){
        foreach(Tab_Button tabBut in tabButtons){
            if (selectedTab != null && tabBut == selectedTab) continue;
            tabBut.background.sprite = exitSprite;
        }
    
    }

    public void OnTabExit(Tab_Button button){
        ResetTabs();
    }
    public void OnTabSelected(Tab_Button button){
        if (selectedTab != null && selectedTab != button) selectedTab.Deselect();
        selectedTab = button;
        button.Select();
        ResetTabs();
        button.background.sprite = selectedSprite;
        int index = button.transform.GetSiblingIndex();
        for(int i = 0; i < objectToSwap.Count; i++){
            if(i == index){
                objectToSwap[i].SetActive(true);
            }
            else{
                objectToSwap[i].SetActive(false);
            }
        }
    }
}
