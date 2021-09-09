using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スコアをを格納するSOクラス
/// </summary>

[CreateAssetMenu(fileName = "Score", menuName = "SO/Score", order = 51)]
public class Score : ScriptableObject
{
    //TODO:List型の初期化がなぜかできない。
    //ラウンドのスコアを保存する。
    [SerializeField]
    private List<float> roundScore = new List<float>();
    public List<float> RoundScore { get; set; }
    //合計点を格納する。
    [SerializeField] 
    private float totalScore= 0;
    public float TotalScore { get; set; }

    void OnEnable()
    {
        Init();
    }

    void OnValidate()
    {
        Init();
    }
    public void OnAfterDeserialize()
    {
        Init();
    }

    void Init()
    {
        RoundScore = roundScore;
        TotalScore = totalScore;
    }
}
