using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Provides the references to the unity components for SingleComponentInstanceLocator
/// </summary>
public class ComponentLocator : MonoBehaviour
{
    public static ComponentLocator instance;
    private Dictionary<Type, object> componentDendencies;
    public SingleObjectInstanceReferences singleObjectInstanceReferences;

    private void Awake()
    {
        instance = this;
        componentDendencies = new Dictionary<Type, object>();
        singleObjectInstanceReferences = new SingleObjectInstanceReferences();
    }

    /// <summary>
    /// MAY NOT BE CALLED IN AWAKE TO ENSURE EVERY OBJECT IS SETUP AND READY TO USE
    /// </summary>
    /// <typeparam name="T">Type of single instance component wanted</typeparam>
    /// <returns></returns>
    public T GetDependency<T>()
    {
        if (!componentDendencies.ContainsKey(typeof(T)))
        {
            var comp = GetComponentInChildren<T>(true);
            componentDendencies.Add(typeof(T), comp);
        }

        return (T) componentDendencies[typeof(T)];
    }

    internal void ReloadScene()
    {
        instance = null;
        singleObjectInstanceReferences = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}