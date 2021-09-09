using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStartEvent", menuName = "GameStartEvent", order = 51)]
public class GameStartEvent : ScriptableObject
{
    private List<GameStartEventListener> listeners = new List<GameStartEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
    }

    public void RegisterListener(GameStartEventListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(GameStartEventListener listener)
    { listeners.Remove(listener); }
}
