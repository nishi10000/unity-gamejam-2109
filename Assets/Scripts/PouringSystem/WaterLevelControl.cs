using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 水位をコントロールする。
/// </summary>
public class WaterLevelControl : MonoBehaviour
{
    public GameObject UkiGameObject;  //水面の上面を示すオブジェクト

    [SerializeField]
    CastingParameter CastingParameter = null;

    //浮きを最初の位置に持ってくる。下から見たときに１以外になった初めての場所。//CastingParameter入力エンド後に実行する。
    public void StartupPoisition()
    {
        CastingParameter.CastingAlpha.ForEach(s => Debug.Log(s));//すべての要素をデバックログに出す。
    }


    private void Update()
    {
        goUp();
    }
    void goUp()
    {
        UkiGameObject.transform.position += new Vector3(0 ,5.0f * Time.deltaTime, 0);

    }
}
