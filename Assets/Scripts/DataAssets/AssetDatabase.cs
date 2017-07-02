using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetDatabase : MonoBehaviour
{

    public Dictionary<Type, Dictionary<string, ScriptableObject>> _assets = new Dictionary<Type, Dictionary<string, ScriptableObject>>();
	
	public static AssetDatabase instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        //This looks so ugly but you need to add a line here for each asset type you want to save/load
        //I put it in this stupid format because I assume there we be a lot of these
		_assets[typeof(PopupAsset)] = new Dictionary<string, ScriptableObject>();
		_assets[typeof(BasicScreenAsset)] = new Dictionary<string, ScriptableObject>();
        foreach (PopupAsset popup in Resources.LoadAll("", typeof(PopupAsset)))
            if (!_assets[typeof(PopupAsset)].ContainsKey(popup.Id))  _assets[typeof(PopupAsset)].Add(popup.Id, popup);
        foreach (BasicScreenAsset popup in Resources.LoadAll("", typeof(BasicScreenAsset)))
			if (!_assets[typeof(BasicScreenAsset)].ContainsKey(popup.Id)) _assets[typeof(BasicScreenAsset)].Add(popup.Id, popup);
    }

    public T GetAsset<T>(string id) where T : ScriptableObject, new()
    {
        var dict = new Dictionary<string, ScriptableObject>();
        if (_assets.TryGetValue(typeof(T), out dict))
        {
            var asset = ScriptableObject.CreateInstance<T>() as ScriptableObject;
            if (dict.TryGetValue(id, out asset))
                return asset as T;
        }
        return new T();
    }
}
