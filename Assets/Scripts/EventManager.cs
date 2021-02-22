using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EventManager 
{
    // why go by type instead of string?
    Dictionary<Type, SGEvent.EventHandle> registeredEventHandles = new Dictionary<Type, SGEvent.EventHandle>();

    public void RegisterListener<T>(SGEvent.EventHandle eventHandle) where T : SGEvent
    {
        var type = typeof(T);
        if(registeredEventHandles.ContainsKey(type))
        {
            registeredEventHandles[type] += eventHandle; //append subscriber to the delegate
        }
        else
        {
            registeredEventHandles.Add(type, eventHandle);
        }      
    }

    public void UnRegisterListener<T>(SGEvent.EventHandle eventHandle) where T : SGEvent
    {
        var type = typeof(T);
        if (!registeredEventHandles.TryGetValue(type, out var handlers)) return;

        handlers -= eventHandle; // unsubscribe from delegate

        if(handlers == null)
        {
            // value for key is empty so clear it
            registeredEventHandles.Remove(type);
        }
        else
        {
            registeredEventHandles[type] = handlers;
        }
    }

    public void FireEvent(SGEvent e)
    {
        var type = e.GetType();
        if(registeredEventHandles.TryGetValue(type, out var listeners))
        {
            listeners(e); //invoking event
        }
    }
}

public abstract class SGEvent
{
    public delegate void EventHandle(SGEvent e);
}

public class GoalScored : SGEvent
{
    public readonly bool bIsRedScore;

    public GoalScored(bool bIsRedScore)
    {
        this.bIsRedScore = bIsRedScore;
    }
}

public class StartGame : SGEvent { }

public class GameOver : SGEvent { }

public class Fouled : SGEvent { }
