using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDepthScaling : MonoBehaviour {

    public float MinimumScale = 0.75f;

	// Update is called once per frame
	void Update () {

        var distance = Mathf.Abs(1 - (Mathf.Abs(transform.position.x) / (Screen.width / 2)));
        transform.localScale = Vector3.one * (1 - ((1 - MinimumScale) * distance));
	}
}
