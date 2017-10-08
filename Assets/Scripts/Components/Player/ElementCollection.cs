using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCollection : MonoBehaviour {

    public GameObject CollectionObject;
    public GameObject TrailingElementPrefab;
    public int MaxElements = 10;

	
	public void AddElement()
    {
        if (CollectionObject.transform.childCount == MaxElements)
            return;

        var element = Instantiate(TrailingElementPrefab);
        element.transform.SetParent(CollectionObject.transform);
        element.GetComponent<TrailingElement>().Player = gameObject;

        var elementType = GetComponent<ElementType>();
        element.GetComponent<ElementType>().CurrentTypes = elementType.CurrentTypes;
    }

    public void RemoveElement()
    {
        if (CollectionObject.transform.childCount > 0)
            Destroy(CollectionObject.transform.GetChild(0).gameObject);
    }
}
