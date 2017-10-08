using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerLives : MonoBehaviour
{

    public ElementType PlayerTypeReference;
    public int StartingLives = 5;
    public int Lives{ 
        get { return _lives; } 
        set 
        {
            _lives = value; 
            UpdateText(_lives);
        }
    }
    private Text _text;
    private int _lives = 0;

    private void Start()
    {
        _text = GetComponent<Text>();
        _lives = StartingLives;
        UpdateText(_lives);
    }

    public void UpdateText(int num)
	{
		if (_text == null)
			return;
        _text.text = num.ToString();
    }

    public void UpdateColor(Color color)
    {
        if (_text == null)
            return;
        _text.color = color;
        _text.SetAllDirty();
    }
}