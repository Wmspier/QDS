using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartView : View
{

    public Button StartButton;
    public Button DataButton;

    // Use this for initialization
    private void Start()
    {
        StartButton.onClick.AddListener(delegate { OnStartPressed(); });
        DataButton.onClick.AddListener(delegate { OnDataPressed(); });
    }

    private void OnStartPressed()
    {
        EventSystem.instance.Dispatch(new NavigationEvents.UnloadScreenEvent());
        EventSystem.instance.Dispatch(new NavigationEvents.LoadSceneEvent(Scenes.Main));
        EventSystem.instance.Dispatch(new NavigationEvents.LoadScreenEvent("MAIN"));
        EventSystem.instance.Dispatch(new GameEvents.InitializeGameEvent());
    }
    private void OnDataPressed()
	{
		EventSystem.instance.Dispatch(new NavigationEvents.LoadScreenEvent("DATA"));
    }
}
