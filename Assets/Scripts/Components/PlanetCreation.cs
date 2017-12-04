using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCreation : MonoBehaviour {

    public PlanetCreationDebug DebugScriptRef;
    public ElementType PlayerTypesRef;
    public int MinElemenetsReq = 5;

    private List<int> _elementDistributionList;

	// Use this for initialization
	void Start () {
        _elementDistributionList = new List<int>(new int[PlayerTypesRef.Types.Count]);
        for (var i = 0; i < PlayerTypesRef.Types.Count; i++)
        {
            if (DebugScriptRef.ElementDistributionList.Count < i)
                break;
            DebugScriptRef.ElementDistributionList[i].text = string.Format("{0}: 0 %", PlayerTypesRef.Types[i].name);
        }
	}

    public int GetTotalCollectedElements()
    {
        int sum = 0;
        _elementDistributionList.ForEach(delegate (int value)
        {
            sum += value;
        });
        Debug.Log("Sum: " + sum);
        return sum;
    }

    public void AddType(int typeIndex, int amount)
    {
        _elementDistributionList[typeIndex] += amount;
        for (var i = 0; i < DebugScriptRef.ElementDistributionList.Count; i++)
        {
            DebugScriptRef.ElementDistributionList[i].text = string.Format("{0}: {1} %",
                                                                           PlayerTypesRef.Types[i].name,
                                                                           Mathf.Round((float)_elementDistributionList[i] /(float)GetTotalCollectedElements() * 100f));
        }
    }
}
