using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : Controller {

    static public GameObject View;

    public PopupController(){
        EventSystem.instance.Connect<PopupEvents.OpenPopupEvent>(OnPopupOpen);
        EventSystem.instance.Connect<PopupEvents.ClosePopupEvent>(OnPopupClose);
    }

    private void InstantiateView() {

        var viewRoot = UnityEngine.Object.FindObjectOfType<Canvas>();

        View = new GameObject("PopupView");
        View.AddComponent<Canvas>();
        View.AddComponent<PopupView>();
        View.transform.SetParent(viewRoot.gameObject.transform,false);
    }

    private void OnPopupOpen(PopupEvents.OpenPopupEvent e)
    {
        if (View == null) InstantiateView();

        View.GetComponent<PopupView>().StackPopup(e.Popup);
    }

    private void OnPopupClose(PopupEvents.ClosePopupEvent e)
    {
        if (View == null) InstantiateView();

        View.GetComponent<PopupView>().ClosePopup();
    }

}
