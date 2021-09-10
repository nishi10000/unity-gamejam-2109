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
    }
}
