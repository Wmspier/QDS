using UnityEngine;

[CreateAssetMenuAttribute(fileName = "Popup Asset", menuName = "Custom/Popup Asset")]
public class PopupAsset : ScriptableObject {

    public string Id;
    public GameObject Popup;
}
