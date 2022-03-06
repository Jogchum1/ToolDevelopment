using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Consider generating this from an string to enum tool for easy editing?
public enum EventType
{
    //ADD STUFF
    VIEW_CHANGED,       //0
    ANOTHER_EVENT,      //1
    NUM_EVENTS          //2
}

public delegate void EventCallback(EventType evt, object value);

public static class EventSystem
{
    private static Dictionary<EventType, List<EventCallback>> eventRegister = new Dictionary<EventType, List<EventCallback>>();

    public static void Subscribe(EventType evt, EventCallback func)
    {
        if (!eventRegister.ContainsKey(evt))
        {
            eventRegister[evt] = new List<EventCallback>();
        }

        eventRegister[evt].Add(func);
    }

    public static void Unsubscribe(EventType evt, EventCallback func)
    {
        if (eventRegister.ContainsKey(evt))
        {
            eventRegister[evt].Remove(func);
        }
    }

    public static void RaiseEvent(EventType evt, object value)
    {
        if (eventRegister.ContainsKey(evt))
        {
            foreach (EventCallback e in eventRegister[evt])
            {
                e.Invoke(evt, value);
            }
        }
    }
}
