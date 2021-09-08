using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 水位をコントロールする。
/// </summary>
public class WaterLevelControl : MonoBehaviour
{
    public GameObject UkiGameObject;  //水面の上面を示すオブジェクト

    public GameObject TargetObject;  //Ukiを置くオブジェクト

    [SerializeField]
    CastingParameter CastingParameter = null;

    //浮きを最初の位置に持ってくる。下から見たときに１以外になった初めての場所。//CastingParameter入力エンド後に実行する。
    public void StartupPoisition()
    {
        //ターゲットの位置にUKIを置く。
        UkiGameObject.transform.position = TargetObject.transform.position;

        //ターゲットの位置の下にUkiを置く。//Rectのサイズを取得する。
        float TagetObjectLowerPos = UkiGameObject.transform.position.y - TargetObject.GetComponent<SpriteRenderer>().bounds.size.y;
        TagetObjectLowerPos = TagetObjectLowerPos / 2;

        //ターゲットのサイズを配列数で分割する。
        float ArrayPartHeight= TargetObject.GetComponent<SpriteRenderer>().bounds.size.y/ CastingParameter.CastingAlpha.Count;

        int CastingLower = 0;  //鋳型の一番下を格納する。
        int CastingUpper = 0;  //鋳型の一番上を格納する。//要確認

        for (int i=0;i< CastingParameter.CastingAlpha.Count; i++)
        {
            if(Mathf.Approximately(CastingParameter.CastingAlpha[i], 1.0f))
            {
                if (i < (CastingParameter.CastingAlpha.Count/2))  //TODO:この場合は半分より小さい図形に対応できない。
                {
                    CastingLower = i;
                }
                else
                {
                    if (CastingUpper == 0)  //最後に入った値ではなく、最初に入った値が欲しい。
                    {
                        CastingUpper = i;
                    }
                }
            }
        }

        //CastingLowerに合わせて高さを足した場所にUkiを設置。
        TagetObjectLowerPos = TagetObjectLowerPos + ArrayPartHeight * CastingLower;
        UkiGameObject.transform.position = new Vector3(UkiGameObject.transform.position.x, TagetObjectLowerPos, UkiGameObject.transform.position.z);
    }


    private void Update()
    {
        //goUp();
    }
    void goUp()
    {
        UkiGameObject.transform.position += new Vector3(0 ,5.0f * Time.deltaTime, 0);
    }
}
