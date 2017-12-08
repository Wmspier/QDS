using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementType : MonoBehaviour {

    public Color AvgSpriteColor;
    public List<Sprite> Types;
    [Range(0,3)]
    public List<int> CurrentTypes= new List<int>();
    public PlayerLives PlayerLivesReference;
    public List<Image> SubtypeImages;
    public Image SingleTypeImage;
    public bool UsedForSpawner;

    public bool SetActivate { set { _active = value; }}

    private bool _active;

    private List<int> _typesInternal = new List<int>();
//    private List<Color> _spriteAvgColors = new List<Color>();

	// Use this for initialization
	private void Awake()
	{
		_typesInternal.Add(CurrentTypes[0]);
		_typesInternal.Add(CurrentTypes[1]);
        //_spriteAvgColors = GetAverageSpriteColors(Types);
        UpdateSprites();
	}
	
	// Upate is called once per frame
	private void Update () {
        if((_typesInternal[1] != CurrentTypes[1] || _typesInternal[0] != CurrentTypes[0]))
		{
			UpdateSprites();
        }
	}

    public void UpdateSprites()
	{
        if (UsedForSpawner)
            return;
		_typesInternal[0] = CurrentTypes[0];
		_typesInternal[1] = CurrentTypes[1];

        var debug = GetComponent<DebugElementCombination>();
        if (debug != null)
            debug.SetType(_typesInternal[0], _typesInternal[1]);

		if (_typesInternal[0] == _typesInternal[1])
		{
			SingleTypeImage.enabled = true;
			SubtypeImages[0].enabled = false;
			SubtypeImages[1].enabled = false;

			SingleTypeImage.overrideSprite = Types[CurrentTypes[0]];
		}
		else
		{
			SingleTypeImage.enabled = false;
			SubtypeImages[0].enabled = true;
			SubtypeImages[1].enabled = true;

			SubtypeImages[0].overrideSprite = Types[CurrentTypes[0]];
			SubtypeImages[1].overrideSprite = Types[CurrentTypes[1]];
		}
;
		//AvgSpriteColor = _spriteAvgColors[_typesInternal[0]];

		if (PlayerLivesReference != null)
			PlayerLivesReference.UpdateColor(AvgSpriteColor);
        
    }

    private List<Color> GetAverageSpriteColors(List<Sprite> sprites)
    {
        var colorList = new List<Color>();
		var spriteCount = sprites.Count;
        for (var i = 0; i < spriteCount; i++)
		{
            var texture = sprites[i].texture;
            var avgColor = ColorUtility.AverageColorFromTexture(texture);
            colorList.Add(avgColor);
        }

        return colorList;
    }

    public void Disable()
    {
        SingleTypeImage.enabled = false;
    }


	public void Enable()
	{
		SingleTypeImage.enabled = true;
	}
}
