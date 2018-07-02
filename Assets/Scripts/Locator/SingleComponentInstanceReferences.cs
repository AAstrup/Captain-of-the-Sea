using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides the references to the unity components for SingleComponentInstanceLocator
/// </summary>
public class SingleComponentInstanceReferences : MonoBehaviour
{
    Dictionary<Type, object> componentDendencies;

    public T GetDependency<T>()
    {
        if (componentDendencies == null)
            componentDendencies = new Dictionary<Type, object>();

        if (!componentDendencies.ContainsKey(typeof(T)))
        {
            var comp = GetComponentInChildren<T>(true);
            componentDendencies.Add(typeof(T), comp);
        }

        return (T) componentDendencies[typeof(T)];
    }

    private void Start()
    {
        SingleObjectInstanceLocator.RegisterComponentReferences(this);
    }
}