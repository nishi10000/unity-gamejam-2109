using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//イベントを受領したら、点数を求める。
public class ScoreCalculation : MonoBehaviour
{
    [SerializeField]
    private CastingParameter CastingParameter = null;

    [SerializeField]
    private Score score = null;

    [SerializeField]
    private float overScorePoint = 0;

    [SerializeField]
    GameLevelSetting gameLevelSetting = null;

    //なぜかScriptableObjectがリセットされない為ここでリセットしてみる。
    private void Start()
    {
        score.RoundScore = new List<float>();
    }

    //Ukiと(CastingParameter.CastingUpperPosition-CastingParameter.CastinglowerPosition)の差異から何パーセントの位置に来ているか確認する。
    //それをScriptableオブジェクトに格納する。
    public void AdditionalScoreCalculation()
    {
        //深さを求める。
        float CastingDepth = CastingParameter.CastingUpperPosition - CastingParameter.CastingLowerPosition;
        float FillingDepth = CastingParameter.CastingWaterLevelPostion - CastingParameter.CastingLowerPosition;
        float FillingRate = FillingDepth / CastingDepth;

        //今のラウンドのGameLevelを持ってくる。
        float Score = gameLevelSetting.GameLevels[gameLevelSetting.NowLevel].BaseScore * FillingRate;
        Score = (float)Math.Round(Score, 1);
        Debug.Log("点数は"+Score+"点です");
        score.RoundScore.Add(Score);
    }

    //はみ出た場合は０を格納する。
    public void WaterLevelOverScoreCalculation()
    {
        Debug.Log("点数は" + overScorePoint + "点です");
        score.RoundScore.Add(overScorePoint);
    }
    //合計点を求める。
    public void TotalScore()
    {
        float? sum = score.RoundScore.Sum();
        score.TotalScore = (float)sum;

        Debug.Log("合計点数は" + sum + "点です");
    }
}
