using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationController : Controller
{
    public ApplicationController()
	{
		EventSystem.instance.Connect<ApplicationEvents.StartUpFinishedEvent>(OnApplicationStart);
    }

    public void OnApplicationStart(ApplicationEvents.StartUpFinishedEvent e)
	{
        //Create a load screen event and dispatch it to be picked up by NavicationController
        EventSystem.instance.Dispatch(new NavigationEvents.LoadSceneEvent(Scenes.Main));
        EventSystem.instance.Dispatch(new NavigationEvents.LoadScreenEvent("START"));
    }
}
