using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IceMilkTea.Core;

public class ScreenManager : MonoBehaviour
{
    // ステートマシン変数の定義、もちろんコンテキストは Player クラス
    private ImtStateMachine<ScreenManager> stateMachine;

    // ステートマシンのイベントID列挙型
    private enum StateEventId
    {
        Title,
        Main,
        Result,
    }

    private void Awake()
    {

        // ステートマシンの遷移テーブルを構築（コンテキストのインスタンスはもちろん自分自身）
        stateMachine = new ImtStateMachine<ScreenManager>(this);
        stateMachine.AddTransition<TitleState, MainState>((int)StateEventId.Main);
        stateMachine.AddTransition<MainState, ResultState>((int)StateEventId.Result);
        stateMachine.AddTransition<ResultState, TitleState>((int)StateEventId.Title);

        // 起動状態はDisabled
        stateMachine.SetStartState<TitleState>();

    }



    private void Start()
    {
        // ステートマシンを起動
        stateMachine.Update();
    }


    // Playerクラスと言っておきながら移動コンポーネントなのでFixedUpdateでステートマシンを回す
    private void Update()
    {
        // ステートマシンの更新
        stateMachine.Update();
    }


    // プレイヤーの操作を有効にします
    public void MainMove()
    {
        // ステートマシンに有効イベントを叩きつける
        stateMachine.SendEvent((int)StateEventId.Main);
    }


    // プレイヤーの操作を無効にします
    public void ResultMove()
    {
        // ステートマシンに無効イベントを叩きつける
        stateMachine.SendEvent((int)StateEventId.Result);
    }
    public void TitleMove()
    {
        // ステートマシンに無効イベントを叩きつける
        stateMachine.SendEvent((int)StateEventId.Title);
    }

    // プレイヤーの移動が許された状態クラス
    private class TitleState : ImtStateMachine<ScreenManager>.State
    {
        protected override void Enter()
        {
            // タイトルシーンを加算する
            SceneManager.LoadScene("0_Title", LoadSceneMode.Additive);
            Debug.Log("TitleState");
        }
    }
    private class MainState : ImtStateMachine<ScreenManager>.State
    {
        protected override void Enter()
        {
            // タイトルシーンを加算する
            SceneManager.LoadScene("1_Main", LoadSceneMode.Additive);
            Debug.Log("MainState");
        }
    }
    private class ResultState : ImtStateMachine<ScreenManager>.State
    {
        protected override void Enter()
        {
            // タイトルシーンを加算する
            SceneManager.LoadScene("2_Result", LoadSceneMode.Additive);
            Debug.Log("ResultState");
        }
    }
}