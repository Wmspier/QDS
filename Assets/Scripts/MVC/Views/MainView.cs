using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : View {

    public Text DifferentCollisionText;
    public Text SameCollisionText;

	private int _diffCollision = 0;
	private int _sameCollision = 0;

	// Use this for initialization
	private void Start () {
        EventSystem.instance.Connect<GameEvents.ElementCollisionEvent>(OnElementCollisison);
	}

    private void OnElementCollisison(GameEvents.ElementCollisionEvent e)
    {
        if(!e.SameType)
        {
            _diffCollision++;
            DifferentCollisionText.text = string.Format("Different Collisions: {0}", _diffCollision);
        }
        else
		{
			_sameCollision++;
			SameCollisionText.text = string.Format("Same Collisions: {0}", _sameCollision);
        }
    }
}
