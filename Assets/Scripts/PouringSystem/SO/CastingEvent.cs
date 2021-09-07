using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 鋳型について更新があったら発火するイベント
/// </summary>

[CreateAssetMenu(fileName = "CastingEvent", menuName = "SO/CastingEvent", order = 51)]
public class CastingEvent : ScriptableObject
{

    private List<CastingEventListener> listeners =
            new List<CastingEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
    }

    public void RegisterListener(CastingEventListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(CastingEventListener listener)
    { listeners.Remove(listener); }
}
