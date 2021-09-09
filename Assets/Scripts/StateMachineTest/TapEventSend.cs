using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapEventSend : MonoBehaviour
{
    [SerializeField]
    CastingEvent GameTapEvent = null;
    [SerializeField]
    CastingEvent GameUnTapEvent = null;
    /// <summary>
    /// 画面タップ時に呼ばれるメソッド
    /// </summary>
    public void TapDown()
    {
        //Debug.Log("画面がタップされた");
        //Invoke("changeScene", 1.0f);    // 一秒後にシーンを切り替えるメソッドを呼び出す
        GameTapEvent.Raise();  //タップイベントを発火
    }
    public void TapUp()
    {
        //Debug.Log("画面がアンタップされた");
        GameUnTapEvent.Raise();  //アンタップイベントを発火
    }

}
