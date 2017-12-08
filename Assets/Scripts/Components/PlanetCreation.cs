using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCreation : MonoBehaviour {

    public PlanetCreationDebug DebugScriptRef;
    public Transform ColorMap;
    public ElementType PlayerTypesRef;
    public int MinElemenetsReq = 5;
    public List<Vector3> ColorCoordinates = new List<Vector3>();
    public int MaxGrowthLevels = 5;
    public int ElementReqForMaxGrowth = 100;
    public float MaxScaleIncrease = 100f;

    private float _scaleIncreasePerGrowthLevel;
    private int _elementReqPerGrowthLevel;
    private List<int> _elementDistributionList;
    private int _totalCollectedElements;
    private Vector3 _colorMapOrigin;

	// Use this for initialization
    void Start () {
        _elementReqPerGrowthLevel = (int)Mathf.Round(ElementReqForMaxGrowth / MaxGrowthLevels);
        _scaleIncreasePerGrowthLevel = MaxScaleIncrease / (float)MaxGrowthLevels;
        _colorMapOrigin = ColorMap.position;
        _elementDistributionList = new List<int>(new int[PlayerTypesRef.Types.Count]);
        for (var i = 0; i < PlayerTypesRef.Types.Count; i++)
        {
            if (DebugScriptRef.ElementDistributionList.Count < i)
                break;
            DebugScriptRef.ElementDistributionList[i].text = string.Format("{0}: 0 %", PlayerTypesRef.Types[i].name);
        }
	}

    public void AddType(int typeIndex, int amount)
    {
        _elementDistributionList[typeIndex] += amount;
        _totalCollectedElements += amount;
        for (var i = 0; i < DebugScriptRef.ElementDistributionList.Count; i++)
        {
            DebugScriptRef.ElementDistributionList[i].text = string.Format("{0}: {1} %",
                                                                           PlayerTypesRef.Types[i].name,
                                                                           Mathf.Round((float)_elementDistributionList[i] /(float)_totalCollectedElements * 100f));
        }

        if(_totalCollectedElements % _elementReqPerGrowthLevel == 0 && _totalCollectedElements <= ElementReqForMaxGrowth)
        {
            StartCoroutine(IncreaseGrowthLevel());
        }

        MoveColorMap(typeIndex);
    }

    private void MoveColorMap(int index)
    {
        ColorMap.localPosition = ColorCoordinates[index];
        return;

        var origin = _colorMapOrigin;
        var difType1 = Mathf.Abs(Vector3.Distance(origin, ColorCoordinates[0]));
        var difType2 = Mathf.Abs(Vector3.Distance(origin, ColorCoordinates[1]));
        var difType3 = Mathf.Abs(Vector3.Distance(origin, ColorCoordinates[2]));
        var difType4 = Mathf.Abs(Vector3.Distance(origin, ColorCoordinates[3]));

        Vector3.MoveTowards(origin, ColorCoordinates[0], (difType1 * Mathf.Round((float)_elementDistributionList[0] / (float)_totalCollectedElements)));
        Vector3.MoveTowards(origin, ColorCoordinates[1], (difType2 * Mathf.Round((float)_elementDistributionList[1] / (float)_totalCollectedElements)));
        Vector3.MoveTowards(origin, ColorCoordinates[2], (difType3 * Mathf.Round((float)_elementDistributionList[2] / (float)_totalCollectedElements)));
        Vector3.MoveTowards(origin, ColorCoordinates[3], (difType4 * Mathf.Round((float)_elementDistributionList[3] / (float)_totalCollectedElements)));

        Debug.LogWarning(string.Format("1:{0} | 2:{1} | 3:{2} | 4:{3}",
                                       (difType1),//* Mathf.Round((float)_elementDistributionList[0] / (float)_totalCollectedElements)),
                                       (difType1),//* Mathf.Round((float)_elementDistributionList[2] / (float)_totalCollectedElements)),
                                       (difType1),//* Mathf.Round((float)_elementDistributionList[1] / (float)_totalCollectedElements)),
                                       (difType1)));//* Mathf.Round((float)_elementDistributionList[3] / (float)_totalCollectedElements))));

        ColorMap.position = origin;
    }

    private IEnumerator IncreaseGrowthLevel()
    {
        var scaleToGrowTo = transform.localScale * (1f +_scaleIncreasePerGrowthLevel);
        while(transform.localScale.x < scaleToGrowTo.x)
        {
            transform.localScale *= 1.001f;
            yield return null;
        }
    }
}
