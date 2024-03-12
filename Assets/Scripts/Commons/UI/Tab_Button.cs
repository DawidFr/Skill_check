using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tab_Button : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Tabs tabs;
    public Image background;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabs.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabs.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabs.OnTabExit(this);
    }

    private void Start()
    {
        background = GetComponent<Image>();
        tabs.Subscribe(this);
    }
}
