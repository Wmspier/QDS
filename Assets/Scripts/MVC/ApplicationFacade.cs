using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationFacade : MonoBehaviour {
    
    public static ApplicationFacade instance;

    private Dictionary<Type, Controller> _controllers;
    private Dictionary<Type, Model> _models;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        _controllers = new Dictionary<Type, Controller>();
        _models = new Dictionary<Type, Model>();
    }

    public void RegisterModel<T>() where T : Model, new()
    {
        if (_models.ContainsKey(typeof(T)))
            return;
        else
            _models.Add(typeof(T), new T());
    }

    public Model GetModel<T>() where T : Model
    {
        Model temp = null;
        _models.TryGetValue(typeof(T), out temp);
        return temp as T;
    }

    public void RegisterController<T>() where T : Controller, new()
    {
        if (_controllers.ContainsKey(typeof(T)))
            return;
        else 
            _controllers.Add(typeof(T), new T());
    }

    private void Update()
    {
        foreach (var controller in _controllers)
            if(controller.Value != null)
                controller.Value.Update();
    }
}
