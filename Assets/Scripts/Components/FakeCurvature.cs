using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCurvature : MonoBehaviour {

    public float VerticalOffset = 10f;
    private float YOrigin;

	// Use this for initialization
	void Start () {
        YOrigin = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {

		var distance = Mathf.Abs(1 - (Mathf.Abs(transform.position.x) / (Screen.width / 2)));
        var newPos = transform.position;
        var offset = VerticalOffset * Screen.height;
        newPos.y = YOrigin + offset * (1 - Mathf.Pow(distance,2));
        transform.position = newPos;
	}
}
