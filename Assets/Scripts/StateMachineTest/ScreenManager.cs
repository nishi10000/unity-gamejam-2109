using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IceMilkTea.Core;

/// <summary>
/// シーン切り替えを管理するクラス
/// </summary>
public class ScreenManager : MonoBehaviour
{
    // ステートマシン変数の定義
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
        // ステートマシンの遷移テーブルを構築
        stateMachine = new ImtStateMachine<ScreenManager>(this);
        stateMachine.AddTransition<TitleState, MainState>((int)StateEventId.Main);
        stateMachine.AddTransition<MainState, ResultState>((int)StateEventId.Result);
        stateMachine.AddTransition<ResultState, TitleState>((int)StateEventId.Title);

        // 起動状態はタイトル
        stateMachine.SetStartState<TitleState>();
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
    public void MainMove()
    {
        stateMachine.SendEvent((int)StateEventId.Main);
    }
    public void ResultMove()
    {
        stateMachine.SendEvent((int)StateEventId.Result);
    }
    public void TitleMove()
    {
        stateMachine.SendEvent((int)StateEventId.Title);
    }

    // タイトルステート
    private class TitleState : ImtStateMachine<ScreenManager>.State
    {
        protected override void Enter()
        {
            // タイトルシーンを加算する
            SceneManager.LoadScene("Title", LoadSceneMode.Additive);
            Debug.Log("TitleStateEnter");
        }
        protected override void Exit()
        {
            // タイトルシーンを減算する
            SceneManager.UnloadSceneAsync("Title");
            Debug.Log("TitleStateExit");
        }
    }

    // メインステート
    private class MainState : ImtStateMachine<ScreenManager>.State
    {
        protected override void Enter()
        {
            // タイトルシーンを加算する
            SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
            Debug.Log("MainStateEnter");
        }
        protected override void Exit()
        {
            // タイトルシーンを減算する
            SceneManager.UnloadSceneAsync("GameScene");
            Debug.Log("MainStateExit");
        }
    }
    // リザルトステート
    private class ResultState : ImtStateMachine<ScreenManager>.State
    {
        protected override void Enter()
        {
            // タイトルシーンを加算する
            SceneManager.LoadScene("Result", LoadSceneMode.Additive);
            Debug.Log("ResultStateEnter");
        }
        protected override void Exit()
        {
            // タイトルシーンを減算する
            SceneManager.UnloadSceneAsync("Result");
            Debug.Log("ResultStateExit");
        }
    }
}