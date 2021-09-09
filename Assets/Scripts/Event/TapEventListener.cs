using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TapEventListener : MonoBehaviour
{
    public TapEvent TapEvent;
    public UnityEvent Response;

    private void OnEnable()
    { TapEvent.RegisterListener(this); }

    private void OnDisable()
    { TapEvent.UnregisterListener(this); }

    public void OnEventRaised()
    { Response.Invoke(); }
}
