using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Button_UI : MonoBehaviour, IPointerClickHandler {
    public Action MouseLeftClickFunc = null;
    public Action MouseRightClickFunc = null;
    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) MouseLeftClickFunc?.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right) MouseRightClickFunc?.Invoke();

    }
}
