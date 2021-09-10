using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 鋳型のパラメーターを格納するSOクラス
/// </summary>

[CreateAssetMenu(fileName = "CastingParameter", menuName = "SO/CastingParameter", order = 51)]
public class CastingParameter : ScriptableObject
{
    //スピードのRateを決める為のアルファ値を格納する。
    [SerializeField] List<float> castingAlpha = new List<float>();
    public List<float> CastingAlpha { get; set; }

    //鋳物の底面のポジションを格納する。
    [SerializeField] float castingLowerPosition = 0;
    public float CastingLowerPosition { get; set; }

    //鋳物の上面のポジションを格納する。
    [SerializeField] float castingUpperPosition = 0;
    public float CastingUpperPosition { get; set; }

    //水面がどのポジションなのか格納する。
    [SerializeField] float castingWaterLevelPostion = 0;
    public float CastingWaterLevelPostion { get; set; }


    void OnEnable()
    {
        Init();
    }

    public void OnAfterDeserialize()
    {
        Init();
    }

    void OnValidate()
    {
        Init();
    }

    void Init()
    {
        CastingAlpha = new List<float>(castingAlpha);

        //CastingAlpha = castingAlpha;
        CastingLowerPosition = castingLowerPosition;
        CastingUpperPosition = castingUpperPosition;
        CastingWaterLevelPostion = castingWaterLevelPostion;
    }
}
