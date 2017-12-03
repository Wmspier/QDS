using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

    public float Speed;

	// Update is called once per frame
	void Update () {

        var pos = transform.position;
        pos.x -= Speed;
        transform.position = pos;

        if (pos.x < -15)
            Destroy(gameObject);
	}
}
