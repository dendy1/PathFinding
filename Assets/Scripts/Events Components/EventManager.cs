using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, EventHandler<EventArgs>> _eventsDictionary;

    private static EventManager _eventManager;
    public static EventManager Instance 
    {
        get
        {
            if (!_eventManager)
            {
                _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!_eventManager)
                {
                    Debug.LogError ("Please, use one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    _eventManager.Initialize();
                }
            }

            return _eventManager;
        }
    }

    private void Initialize()
    {
        if (_eventsDictionary == null)
            _eventsDictionary = new Dictionary<string, EventHandler<EventArgs>>();
    }

    public static void SubscribeToEvent(string eventName, EventHandler<EventArgs> listener)
    {
        EventHandler<EventArgs> currentEvent;

        if (Instance._eventsDictionary.TryGetValue(eventName, out currentEvent))
        {
            currentEvent += listener;
            Instance._eventsDictionary[eventName] = currentEvent;
        }
        else
        {
            currentEvent += listener;
            Instance._eventsDictionary.Add(eventName, currentEvent);
        }
    }

    public static void UnsubscribeFromEvent(string eventName, EventHandler<EventArgs> listener)
    {
        EventHandler<EventArgs> currentEvent;

        if (Instance._eventsDictionary.TryGetValue(eventName, out currentEvent))
        {
            currentEvent -= listener;
            Instance._eventsDictionary[eventName] = currentEvent;
        }
    }

    public static void InvokeEvent(string eventName, object sender, EventArgs args)
    {
        EventHandler<EventArgs> currentEvent;

        if (Instance._eventsDictionary.TryGetValue(eventName, out currentEvent))
        {
            currentEvent.Invoke(sender, args);
        }
    }
}
