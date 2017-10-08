using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementCombination : MonoBehaviour {

    private ElementType _combinedElement;
    private GameObject _otherElement;
    private bool _combineCooldown;

    public bool Combined = false;
    public GameObject CombinedElementPrefab;
    public float DetachThreshold = 10f;
    public float CombinedCooldown = 5f;

    public void Combine(ElementType otherElement)
    {
        if (otherElement.transform.position.x > transform.position.x)
            return;

        if (Combined || _combineCooldown)
            return;

        _combinedElement = otherElement;
        _otherElement = otherElement.gameObject;

        var elementType = GetComponent<ElementType>();
        CreateCombinedElement(elementType.CurrentTypes[0], otherElement.CurrentTypes[0]);

        elementType.Disable();
        _otherElement.GetComponent<ElementType>().Disable();


        Combined = true;
    }

    public void CreateCombinedElement(int type1, int type2)
    {
        var combinedElement = Instantiate(CombinedElementPrefab) as GameObject;

        var middlePosition = transform.position;
        middlePosition.x += (_otherElement.transform.position.x - transform.position.x) / 2;

        combinedElement.transform.position = middlePosition;
        combinedElement.transform.SetParent(transform);
        combinedElement.transform.localScale = Vector3.one;
        _combinedElement = combinedElement.GetComponent<ElementType>();

        _combinedElement.CurrentTypes = new List<int>() { type1, type2 };
    }

    private void Update()
	{
        if (!Combined || _combineCooldown)
            return;
        
		var middlePosition = transform.position;
		middlePosition.x += (_otherElement.transform.position.x - transform.position.x) / 2;
		_combinedElement.transform.position = middlePosition;

        var playerMovement = GetComponent<PlayerMovement>();
        var otherMovement = _otherElement.GetComponent<PlayerMovement>();

        if (playerMovement.IsLerping)
			otherMovement.SetLerpManual(playerMovement.LerpDirection);
		if (otherMovement.IsLerping)
			playerMovement.SetLerpManual(otherMovement.LerpDirection);

        if (Mathf.Abs(_otherElement.transform.position.x - transform.position.x) > DetachThreshold)
        {
            Detach();
        }
    }

    private void Detach()
    {
        transform.position = _combinedElement.transform.position;
		Destroy(_combinedElement.gameObject);

		Combined = false;
		_combineCooldown = true;

        _otherElement.GetComponent<ElementType>().Enable();
        GetComponent<ElementType>().Enable();

        StartCoroutine(CombineCooldown());
    }

    private IEnumerator CombineCooldown()
    {
        yield return new WaitForSeconds(CombinedCooldown);
        _combineCooldown = false;
    }

}
