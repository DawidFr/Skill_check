using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tabs : MonoBehaviour
{
    public List<Tab_Button> tabButtons;
    
    public void Subscribe(Tab_Button button){
        tabButtons ??= new List<Tab_Button>();
        tabButtons.Add(button);
    }

    public void OnTabEnter(Tab_Button button){
        
    }
    public void OnTabExit(Tab_Button button){
        
    }
    public void OnTabSelected(Tab_Button button){
        
    }
}
