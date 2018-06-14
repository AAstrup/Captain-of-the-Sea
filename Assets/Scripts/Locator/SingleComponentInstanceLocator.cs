using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows any component at any time to register for a callback returning unity component dependencies
/// Returns components there is a single instance of
/// </summary>
public class SingleComponentInstanceLocator {

    // References
    private static SingleComponentInstanceLocator instance;
    private static SingleComponentInstanceLocator Instance
    {
        get
        {
            if (instance == null)
                instance = new SingleComponentInstanceLocator();
            return instance;
        }
    }
    private SingleComponentInstanceLocator() { }
    public SingleComponentInstanceReferences componentReferences;

    // Callback
    public delegate void DependenciesReadyEvent(SingleComponentInstanceLocator locator);
    public DependenciesReadyEvent dependenciesReadyEvent;

    /// <summary>
    /// Register component references
    /// Provided from a unity behaviour
    /// </summary>
    /// <param name="singleComponentInstanceBehaviour"></param>
    public static void RegisterComponentReferences(SingleComponentInstanceReferences singleComponentInstanceBehaviour)
    {
        Instance.componentReferences = singleComponentInstanceBehaviour;
        if (Instance.dependenciesReadyEvent != null)
            Instance.dependenciesReadyEvent(instance);
    }

    /// <summary>
    /// Subscription is invoked when dependencies are loaded
    /// </summary>
    /// <param name="callback"></param>
    public static void SubscribeToDependenciesCallback(DependenciesReadyEvent callback)
    {
        if (Instance.IsReady())
            callback(Instance);
        else
            Instance.dependenciesReadyEvent += callback;
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
        SubscribeToDependenciesCallback(delegate (SingleComponentInstanceLocator singleComponentInstanceLocator) { behaviour.enabled = true; });
    }
}
