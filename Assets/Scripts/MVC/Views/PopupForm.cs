using UnityEngine;
using UnityEngine.UI;

public class PopupForm : MonoBehaviour {

    public Button CloseButton;
    public Button BgCloseButton;

    private void Start() {

        var e = new PopupEvents.ClosePopupEvent();
        CloseButton.onClick.AddListener(delegate {
                EventSystem.instance.Dispatch(e);
        });
        BgCloseButton.onClick.AddListener(delegate
        {
            EventSystem.instance.Dispatch(e);
        });
    }
}
