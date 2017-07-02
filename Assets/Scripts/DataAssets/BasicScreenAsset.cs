using UnityEngine;

[CreateAssetMenuAttribute(fileName = "Screen", menuName = "Custom/Basic Screen")]
public class BasicScreenAsset : ScriptableObject {

    public string Id;
    public GameObject ScreenPrefab;
}
