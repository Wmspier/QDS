using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Text.RegularExpressions;

public class TestPageReader : MonoBehaviour {

    public Text TextVisual;

	// Use this for initialization
	void Start()
	{
//		GetPageData();
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void GetPageData()
	{
//		string url = "http://services.swpc.noaa.gov/text/27-day-outlook.txt";
//		string matchPattern = @"data-loc=""(?<lat>.*?),(?<long>.*?)"""; ;


//		if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(matchPattern))
//		{
//			Debug.LogErrorFormat("Yo that shit empt");
//			return;
//		}

//        var testUrl = new WWW(url);

//#if UNITY_EDITOR
//		// In the editor coroutines don't update as the update tick doesn't run.
//		// So just stall until we have the text, for a maximum of four seconds.
//		var timeOut = 4000;
//		while (!testUrl.isDone)
//		{
//			Thread.Sleep(1);
//			if (--timeOut <= 0)
//			{
//				Debug.LogError("Connection Time-out");
//				return;
//			}
//		}
//#else
//        yield return ipLocation;
//#endif

	//	if (!string.IsNullOrEmpty(testUrl.error))
	//	{
	//		Debug.LogErrorFormat("Page is Empty!");
	//		return;
	//	}

	//	Debug.Log("Connection Successful");
	//	TextVisual.text = testUrl.text;

	//	//Match m = Regex.Match(testUrl.text, "((" + Regex.Escape("UTC") + ").*?){" + 0 + "}");


	//	var f = testUrl.text.LastIndexOf("UTC", System.StringComparison.Ordinal);
	//	TextVisual.text = testUrl.text.Substring(f-5);
	}
}
