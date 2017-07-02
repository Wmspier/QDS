using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : View
{

    public Button StartButton;

    // Use this for initialization
    private void Start()
    {
        StartButton.onClick.AddListener(delegate{OnStartPressed();});
    }

    private void OnStartPressed()
    {
		EventSystem.instance.Dispatch(new NavigationEvents.UnloadScreenEvent());
		EventSystem.instance.Dispatch(new NavigationEvents.LoadSceneEvent(Scenes.Main));
		EventSystem.instance.Dispatch(new NavigationEvents.LoadScreenEvent("MAIN"));
        EventSystem.instance.Dispatch(new GameEvents.InitializeGameEvent());
    }
}
