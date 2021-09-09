using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;


/// <summary>
/// ゲームシーンの状態を管理する。
/// </summary>
public class GameFSM : MonoBehaviour
{
    static public GameFSM instance;
    public CastingEvent WaterLevelUpStartEvent = null;
    public CastingEvent WaterLevelUpStopEvent = null;

    //鋳型を格納する変数。TODO:この数だけ、ラウンドが行われる。
    [SerializeField]
    private List<GameObject> CastingGameObjects = new List<GameObject>();
    public int Round = 0;  //ゲームのラウンドをカウントする。

    public enum StateEventId
    {
        Play,
        Miss,
        Stop,
        Exit,
        Finish
    }

    private ImtStateMachine<GameFSM> stateMachine;

    private void Awake()
    {
        // ステートマシンのインスタンスを生成して遷移テーブルを構築
        stateMachine = new ImtStateMachine<GameFSM>(this); // 自身がコンテキストになるので自身のインスタンスを渡す
        stateMachine.AddTransition<EntryState, MoldCountConfirmationState>((int)StateEventId.Finish);
        stateMachine.AddTransition<MoldCountConfirmationState, MoldEntryState>((int)StateEventId.Finish);
        stateMachine.AddTransition<MoldEntryState, UpWaterLevelState>((int)StateEventId.Play);
        stateMachine.AddTransition<UpWaterLevelState, WaterLevelOverState>((int)StateEventId.Miss);
        stateMachine.AddTransition<UpWaterLevelState, StopWaterLevelUpwardState>((int)StateEventId.Stop);
        stateMachine.AddTransition<WaterLevelOverState, ScoreCalculationState>((int)StateEventId.Finish);
        stateMachine.AddTransition<StopWaterLevelUpwardState, ScoreCalculationState>((int)StateEventId.Finish); 
        stateMachine.AddTransition<ScoreCalculationState, TotalScoreCalculationState>((int)StateEventId.Finish);
        stateMachine.AddTransition<TotalScoreCalculationState, MoldCountConfirmationState>((int)StateEventId.Finish);
        stateMachine.AddTransition<MoldCountConfirmationState, GameEndState>((int)StateEventId.Exit);

        // 起動ステートを設定（起動ステートは EntryState）
        stateMachine.SetStartState<EntryState>();

        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        // ステートマシンを起動
        stateMachine.Update();
    }

    private void Update()
    {
        // ステートマシンの更新
        stateMachine.Update();
    }


    //起動時時
    private class EntryState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowEntryState");
            StateMachine.SendEvent((int)StateEventId.Finish);
        }
    }
    //鋳型残数確認
    private class MoldCountConfirmationState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowMoldCountConfirmationState");
            if (GameFSM.instance.CastingGameObjects.Count > GameFSM.instance.Round)
            {
                StateMachine.SendEvent((int)StateEventId.Finish);
                
            }
            else
            {
                StateMachine.SendEvent((int)StateEventId.Exit);
            }
        }

    }
    //ゲーム中のタップイベントを受領する。そしてステートをUpWaterLevelに変更する。
    public void  ReciveTapEvent()
    {
        stateMachine.SendEvent((int)StateEventId.Play);
    }
    //鋳型出現//TODO:鋳型の出現依頼イベント送付する。
    private class MoldEntryState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowMoldEntryState");

            //GameObject obj = (GameObject)Resources.Load("Cube");
            GameObject obj = GameFSM.instance.CastingGameObjects[0];//TODO:ここをランダムにしても面白かも。

            // プレハブを元にオブジェクトを生成する
            GameObject instance = (GameObject)Instantiate(obj,
                                                          new Vector3(5.0f, 0.0f, 0.0f),
                                                          Quaternion.identity);
        }
    }
    //上昇水位を止める。
    public void ReciveUnTapEvent()
    {
        stateMachine.SendEvent((int)StateEventId.Stop);
    }
    //水位上昇//イベントを発火させる。
    public class UpWaterLevelState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowUpWaterLevelState");
            GameFSM.instance.WaterLevelUpStartEvent.Raise();
        }
    }
    //水位オーバー//TODO:水位オーバーへの遷移は、鋳型のクラスからイベントを送付して遷移する。
    private class WaterLevelOverState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowWaterLevelOverState");
            //GameFSM.instance.WaterLevelUpStopEvent.Raise();
        }
    }

    //水位ストップ（得点圏）
    private class StopWaterLevelUpwardState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowStopWaterLevelUpwardState");
            GameFSM.instance.WaterLevelUpStopEvent.Raise();
            StateMachine.SendEvent((int)StateEventId.Finish);
        }
    }
    //単体の得点算出
    private class ScoreCalculationState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowScoreCalculationState");
            GameFSM.instance.Round++;  //ラウンドをカウントする。
            StateMachine.SendEvent((int)StateEventId.Finish);
        }
    }
    //総得点算出
    private class TotalScoreCalculationState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowTotalScoreCalculationState");
            StateMachine.SendEvent((int)StateEventId.Finish);
        }
    }
    //ゲーム終了/スコア画面に
    private class GameEndState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowGameEndState");
            //TODO:ゲーム終了イベントをRaiseする。
        }
    }
}
