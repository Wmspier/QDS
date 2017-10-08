using UnityEngine;
using UnityEngine.UI;

public class DebugElementCombination : MonoBehaviour {

	public Text DebugText;

	public void SetType(int type0, int type1)
	{
		DebugText.text = string.Format("{0}|{1}", type0, type1);
	}
}
