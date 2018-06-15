using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows any component at any time to register for a callback returning unity component dependencies
/// Returns components there is a single instance of
/// </summary>
public class SingleObjectInstanceLocator {

    // Instance management
    private static SingleObjectInstanceLocator instance;
    private static SingleObjectInstanceLocator GetInstance()
    {
        if (instance == null)
        {
            instance = new SingleObjectInstanceLocator();
            instance.objectReferences.SetupDependentObjects();
        }
        return instance;
    }

    private SingleObjectInstanceLocator() {
        objectReferences = new SingleObjectInstanceReferences();
    }

    // Instances that it has located
    public SingleComponentInstanceReferences componentReferences;
    public SingleObjectInstanceReferences objectReferences;

    // Callback
    public delegate void DependenciesReadyEvent(SingleObjectInstanceLocator locator);
    public DependenciesReadyEvent dependenciesReadyEvent;

    /// <summary>
    /// Register component references
    /// Provided from a unity behaviour
    /// </summary>
    /// <param name="singleComponentInstanceBehaviour"></param>
    public static void RegisterComponentReferences(SingleComponentInstanceReferences singleComponentInstanceBehaviour)
    {
        GetInstance().componentReferences = singleComponentInstanceBehaviour;
        if (GetInstance().dependenciesReadyEvent != null)
            GetInstance().dependenciesReadyEvent(instance);
    }

    /// <summary>
    /// Subscription is invoked when dependencies are loaded
    /// </summary>
    /// <param name="callback"></param>
    public static void SubscribeToDependenciesCallback(DependenciesReadyEvent callback)
    {
        if (GetInstance().IsReady())
            callback(GetInstance());
        else
            GetInstance().dependenciesReadyEvent += callback;
    }

    private bool IsReady()
    {
        return componentReferences != null;
    }

    /// <summary>
    /// Disables the behaviour until dependencies are ready
    /// When dependencies are ready it invokes the callback and enables the behaviour
    /// </summary>
    /// <param name="callback">Invoked when dependencies are loaded</param>
    /// <param name="behaviour">To disable until dependencies are available</param>
    public static void SubscribeToDependenciesCallback(DependenciesReadyEvent callback, Behaviour behaviour)
    {
        behaviour.enabled = false;
        SubscribeToDependenciesCallback(callback);
        SubscribeToDependenciesCallback(delegate (SingleObjectInstanceLocator singleComponentInstanceLocator) { behaviour.enabled = true; });
    }

    internal static void ReloadScene()
    {
        instance = null;
    }
}
