using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IceMilkTea.Core;

/// <summary>
/// �V�[���؂�ւ����Ǘ�����N���X
/// </summary>
public class ScreenManager : MonoBehaviour
{
    // �X�e�[�g�}�V���ϐ��̒�`
    private ImtStateMachine<ScreenManager> stateMachine;

    // �X�e�[�g�}�V���̃C�x���gID�񋓌^
    private enum StateEventId
    {
        Title,
        Main,
        Result,
    }

    private void Awake()
    {
        // �X�e�[�g�}�V���̑J�ڃe�[�u�����\�z
        stateMachine = new ImtStateMachine<ScreenManager>(this);
        stateMachine.AddTransition<TitleState, MainState>((int)StateEventId.Main);
        stateMachine.AddTransition<MainState, ResultState>((int)StateEventId.Result);
        stateMachine.AddTransition<ResultState, TitleState>((int)StateEventId.Title);

        // �N����Ԃ̓^�C�g��
        stateMachine.SetStartState<TitleState>();
    }
    private void Start()
    {
        // �X�e�[�g�}�V�����N��
        stateMachine.Update();
    }
    private void Update()
    {
        // �X�e�[�g�}�V���̍X�V
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

    // �^�C�g���X�e�[�g
    private class TitleState : ImtStateMachine<ScreenManager>.State
    {
        protected override void Enter()
        {
            // �^�C�g���V�[�������Z����
            SceneManager.LoadScene("Title", LoadSceneMode.Additive);
            Debug.Log("TitleStateEnter");
        }
        protected override void Exit()
        {
            // �^�C�g���V�[�������Z����
            SceneManager.UnloadSceneAsync("Title");
            Debug.Log("TitleStateExit");
        }
    }

    // ���C���X�e�[�g
    private class MainState : ImtStateMachine<ScreenManager>.State
    {
        protected override void Enter()
        {
            // �^�C�g���V�[�������Z����
            SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
            Debug.Log("MainStateEnter");
        }
        protected override void Exit()
        {
            // �^�C�g���V�[�������Z����
            SceneManager.UnloadSceneAsync("GameScene");
            Debug.Log("MainStateExit");
        }
    }
    // ���U���g�X�e�[�g
    private class ResultState : ImtStateMachine<ScreenManager>.State
    {
        protected override void Enter()
        {
            // �^�C�g���V�[�������Z����
            SceneManager.LoadScene("Result", LoadSceneMode.Additive);
            Debug.Log("ResultStateEnter");
        }
        protected override void Exit()
        {
            // �^�C�g���V�[�������Z����
            SceneManager.UnloadSceneAsync("Result");
            Debug.Log("ResultStateExit");
        }
    }
}