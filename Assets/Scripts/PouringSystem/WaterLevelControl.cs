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

    private float TagetObjectLowerPos = 0;  //鋳型底面
    private float TagetObjectUpperPos = 0;　//鋳型上面

    private int CastingLower = 0;  //鋳型の一番下を格納する。
    private int CastingUpper = 0;  //鋳型の一番上を格納する。//要確認


    //ターゲットのサイズを配列数で分割したものを格納する。1配列あたりの高さを算出。
    private float ArrayPartHeight = 0;

    //クリックしているかどうかを判断（仮）
    public bool WaterLevelUpStart = false;

    //水位が上がるベースの速度
    [SerializeField]
    private float LevelSpeed = 5.0f;

    [SerializeField]
    private float RateMagnification = 1.0f;  //形状に対してどれほど大げさに上昇率を変えるかを格納。


    //鋳型形状確認後発火//CastingParameter入力エンド後に実行する。
    public void Setup()
    {
        //鋳型の底面と上面を確認する。
        WaterLevelPoisitionAnalys();
        //鋳型の底面にUKIをもってくる。
        SetStartupPoisition(TagetObjectLowerPos);
    }


    //鋳型の底面と上面を確認する
    void WaterLevelPoisitionAnalys()
    {

        CastingLower = 0;  //初期化
        CastingUpper = 0;  //初期化
        //ターゲットの位置のRect下の場所を確認。//Rectのサイズを取得する。
        TagetObjectLowerPos = TargetObject.transform.position.y - TargetObject.GetComponent<SpriteRenderer>().bounds.size.y;
        TagetObjectLowerPos = TagetObjectLowerPos / 2;

        TagetObjectUpperPos = TargetObject.transform.position.y + TargetObject.GetComponent<SpriteRenderer>().bounds.size.y;
        TagetObjectUpperPos = TagetObjectUpperPos / 2;

        //ターゲットのサイズを配列数で分割する。1配列あたりの高さを算出。
        ArrayPartHeight = TargetObject.GetComponent<SpriteRenderer>().bounds.size.y / CastingParameter.CastingAlpha.Count;

        
        for (int i = 0; i < CastingParameter.CastingAlpha.Count; i++)
        {
            if (Mathf.Approximately(CastingParameter.CastingAlpha[i], 1.0f))
            {
                if (i < (CastingParameter.CastingAlpha.Count / 2))  //TODO:この場合は半分より小さい図形に対応できない。
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

        //鋳型上面を算出
        TagetObjectUpperPos = TagetObjectUpperPos - (ArrayPartHeight * (CastingParameter.CastingAlpha.Count - CastingUpper));
    }

    //浮きを最初の位置に持ってくる。下から見たときに１以外になった初めての場所。
    public void SetStartupPoisition(float TagetObjectLowerPos)
    {
        //ターゲットの位置にUKIを置く。
        UkiGameObject.transform.position = TargetObject.transform.position;
        UkiGameObject.transform.position = new Vector3(UkiGameObject.transform.position.x, TagetObjectLowerPos, UkiGameObject.transform.position.z);
    }

    private void Update()
    {
        if (WaterLevelUpStart)
        {
            WaterLevelUp();
        }
        //途中で、Onbuttonが止まると,得点判定。//それは別クラスか？
    }

    //スタートイベントを受けて動作開始。
    //鋳型上面に向けて、Ukiを移動させる。
    //Ukiが上面を超えたら、NGイベント発火。
    void WaterLevelUp()
    {
        float Rate = 0;

        //今のUkiの位置を確認する。
        float nowHight = UkiGameObject.transform.position.y;

        //初期値と今の高さを比べてどのぐらい上がったかを確認する。
        float UpwardQuantity = nowHight - TagetObjectLowerPos;
        
        //上昇量に応じて配列の数字を上げる。
        int ArrayForRaise =Mathf.FloorToInt(UpwardQuantity / ArrayPartHeight);

        //配列に対して飛び越えないようにする。
        if(CastingParameter.CastingAlpha.Count > (CastingLower + ArrayForRaise)) { 
            Rate = CastingParameter.CastingAlpha[CastingLower + ArrayForRaise];
        }
        //TODO:思ったより速度の変化が分かりずらい。
        Rate = RateMagnification * Rate;
        //Debug.Log(Rate);

        //その後TagetObjectUpperPosを超えると、NGイベント発火。
        UkiGameObject.transform.position += new Vector3(0, LevelSpeed * Time.deltaTime * Rate, 0);
    }
}
