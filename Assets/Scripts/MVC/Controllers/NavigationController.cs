using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationController : Controller {

    private Stack<GameObject> _screenStack = new Stack<GameObject>();

    public NavigationController()
    {
        EventSystem.instance.Connect<NavigationEvents.LoadScreenEvent>(OnLoadScreen);
        EventSystem.instance.Connect<NavigationEvents.UnloadScreenEvent>(OnUnloadScreen);
    }

	public void OnLoadScreen(NavigationEvents.LoadScreenEvent e)
	{
        if(_screenStack.Count > 0 && _screenStack.Peek().GetComponent<ScrollableView>())
        {
            _screenStack.Peek().GetComponent<ScrollableView>().ScrollView.enabled = false;
        }

        var NewScreenAsset = AssetDatabase.instance.GetAsset<BasicScreenAsset>(e.Id);
        var NewScreen = GameObject.Instantiate(NewScreenAsset.ScreenPrefab);
        NewScreen.name = NewScreenAsset.Id;
        _screenStack.Push(NewScreen);
	}

	public void OnUnloadScreen(NavigationEvents.UnloadScreenEvent e)
	{
        if (_screenStack.Count == 0) return;
		GameObject.Destroy(_screenStack.Pop());
		if (_screenStack.Count == 0) return;

		if (_screenStack.Peek().GetComponent<ScrollableView>())
		{
			_screenStack.Peek().GetComponent<ScrollableView>().ScrollView.enabled = true;
		}
	}

    public void OnLoadScene(NavigationEvents.LoadSceneEvent e)
	{
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(e.Scene);
    }
}
