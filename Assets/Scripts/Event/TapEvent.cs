using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TapEvent", menuName = "TapEvent", order = 51)]
public class TapEvent : ScriptableObject
{
    private List<TapEventListener> listeners = new List<TapEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
    }

    public void RegisterListener(TapEventListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(TapEventListener listener)
    { listeners.Remove(listener); }
}
