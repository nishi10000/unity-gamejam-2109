using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームシーンのLevelを格納する。
/// GamelLevelのSOを格納する。
/// </summary>
[CreateAssetMenu(fileName = "GameLevelSetting", menuName = "SO/GameLevelSetting", order = 51)]
public class GameLevelSetting : ScriptableObject
{
    [SerializeField]
    private List<GameLevel> gameLevels = null;
    public List<GameLevel> GameLevels { get; set; }

    //ラウンド数を決める。
    [SerializeField]
    private float totalRound = 1;
    public float TotalRound { get; set; }

    //選択中のレベルを格納する。
    [SerializeField]
    private int   nowLevel = 0;
    public int NowLevel { get; set; }


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
        GameLevels = new List<GameLevel>(gameLevels);
        TotalRound = totalRound;
        NowLevel = nowLevel;
    }
}
