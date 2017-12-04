using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCollection : MonoBehaviour {

    public GameObject CollectionObject;
    public GameObject TrailingElementPrefab;
    public PlanetCreation PlanetCreationRef;
    public int MaxElements = 10;

	public void AddElement()
    {  
        var element = Instantiate(TrailingElementPrefab);
        element.transform.SetParent(CollectionObject.transform);
        element.GetComponent<TrailingElement>().Player = gameObject;

        var elementType = GetComponent<ElementType>();
        element.GetComponent<ElementType>().CurrentTypes = elementType.CurrentTypes;

        if (CollectionObject.transform.childCount >= PlanetCreationRef.MinElemenetsReq)
        { 
            SendElementsToPlanet();
            return;
        } 
    }

    public void RemoveElement()
    {
        if (CollectionObject.transform.childCount > 0)
            Destroy(CollectionObject.transform.GetChild(0).gameObject);
    }

    private void SendElementsToPlanet()
    {
        var elementType = GetComponent<ElementType>();
        PlanetCreationRef.AddType(elementType.CurrentTypes[0], CollectionObject.transform.childCount);
        foreach(Transform child in CollectionObject.transform)
            Destroy(child.gameObject);
        
    }
}
