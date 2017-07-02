using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupView : View {

    private Stack<GameObject> _popupContainer = new Stack<GameObject>();
    private RectTransform _transform;
    private Button _bgClose;
    private GraphicRaycaster _bgRaycast;

    private void Awake()
    {

        _transform = GetComponent<RectTransform>();

        _transform.offsetMin = Vector2.zero;
        _transform.offsetMax = Vector2.zero;
        _transform.anchorMin = Vector2.zero;
        _transform.anchorMax = new Vector2(1, 1);
    }

    public void StackPopup(PopupAsset popup){ 
        
        _popupContainer.Push(Instantiate(popup.Popup));
        _popupContainer.Peek().transform.SetParent(transform, false);
    }

    public void ClosePopup()
    { 
        Destroy(_popupContainer.Pop());
    }
}
