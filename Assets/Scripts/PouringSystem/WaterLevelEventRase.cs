using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//イベントが正しく動くか確認用のサンプルのスクリプト
public class WaterLevelEventRase : MonoBehaviour
{
    [SerializeField]
    private CastingEvent StartEvent;
    [SerializeField]
    private CastingEvent StopEvent;

    public void OnMouseButton()
    {
        StartEvent.Raise();

    }
    public void OnReaseButton()
    {
        StopEvent.Raise();

    }
}
