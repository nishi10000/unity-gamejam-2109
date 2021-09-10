using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SOによって、ゲームシーンのPrefabオブジェクトを格納する。
/// また、Prefabの点数も格納する。
/// </summary>
/// 
[CreateAssetMenu(fileName = "GameLevel", menuName = "SO/GameLevel", order = 51)]

public class GameLevel : ScriptableObject
{
    [SerializeField]
    private GameObject castingObject = null;
    public GameObject CastingObject { get; set; }

    [SerializeField]
    private string name = null;
    public string Name { get; set; }

    [SerializeField]
    private int baseScore = 100;
    public int BaseScore { get; set; }

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
        CastingObject = castingObject;
        Name = name;
        BaseScore = baseScore;
    }
}
