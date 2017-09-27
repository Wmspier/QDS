using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementCombination : MonoBehaviour {

    private ElementType _combinedElement;

    public bool Combined = false;

    public void Combine(ElementType otherElement)
    {
        if (otherElement.transform.position.x > transform.position.x)
            return;

        _combinedElement = otherElement;


    }

}
