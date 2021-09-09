using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStartEventListener : MonoBehaviour
{
    public GameStartEvent GameStartEvent;
    public UnityEvent Response;

    private void OnEnable()
    { GameStartEvent.RegisterListener(this); }

    private void OnDisable()
    { GameStartEvent.UnregisterListener(this); }

    public void OnEventRaised()
    { Response.Invoke(); }
}
