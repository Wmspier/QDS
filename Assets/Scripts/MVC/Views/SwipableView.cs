using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipableView : ScrollableView {

    [Tooltip("Distance from center to begin fade")]
    [Range(0,1)]
	public float FadeDistance;
    private float _fadeDistance;
	[Tooltip("Distance from center to begin be destroyed")]
	[Range(0, 1)]
	public float DestroyDistance;
    private float _destroyDistance;
	[Tooltip("The scrolling view content")]
    public GameObject Content;

    private Vector3 _origin;

    public void Start()
    {
        _origin = Content.GetComponent<RectTransform>().anchoredPosition;
        _fadeDistance = FadeDistance * Screen.width/2;
        _destroyDistance = DestroyDistance * Screen.width/2;
    }

	// Update is called once per frame
	public void Update()
	{
        //Find the position of the scrolling content and its distance from the origin
		var Position = Content.GetComponent<RectTransform>().anchoredPosition;
		var Distance = Vector3.Distance(Position, _origin);
        //If the distance is too little return, no reason to iterate through images
        if (Distance < .1f) return;
        //If the distance is meets or exceedes the destruction distance, dispatch an UnloadScreen event
        if (Distance >= _destroyDistance) EventSystem.instance.Dispatch(new NavigationEvents.UnloadScreenEvent());
        //Otherwise find an appropriate alpha (fade distance = 1.0, destroy distance = 0.0)
        var a = 1 - Mathf.Clamp(((Distance - _fadeDistance) / (_destroyDistance - _fadeDistance)), 0f, 1f);
        //Iterate through all children with image components and set their alphas accordingly
        foreach (var image in transform.GetComponentsInChildren<Image>(false))
		{
            //Create a temp var because you can't directly modifiy alpha values
			var color = image.color;
            color.a = a;
            image.color = color;
		}
	}
}
