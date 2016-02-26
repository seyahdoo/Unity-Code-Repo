using UnityEngine;
using System.Collections;


/// <summary>
/// Usage: 
/// To Subscribe: EventManager.SampleEvent += YourCustomMethod;
/// To Trigger: EventManager.SampleEventTrigger();
/// 
/// For using custom Parameters, mock up with delegate
/// Example:
/// public delegate int CustomDelegateName(string ParamName)
/// 
/// </summary>
public class EventManager : Singleton<EventManager> {



    public delegate void VoidDelegate();
    
    /// <summary>
    /// Usage: EventManager.SampleEvent += MethodToSubscribe;
    /// </summary>
    public static event VoidDelegate SampleEvent;

    /// <summary>
    /// Usage: EventManager.SampleEventTrigger();
    /// </summary>
    public static void SampleEventTrigger()
    {
        //call all methods that subscribed to my event
        //if there is one?
        if (SampleEvent != null)
            SampleEvent();
        
    }

    public static event VoidDelegate GameOverEvent;
    public static void GameOverTrigger()
    {
        if (GameOverEvent != null)
            GameOverEvent();
    }

    public static event VoidDelegate GameStartEvent;
    public static void GameStartTrigger()
    {
        if (GameStartEvent != null)
            GameStartEvent();
    }



}
