using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 画面タップ時に呼ばれるメソッド
    /// </summary>
    public void TapDown()
    {
        Debug.Log("画面がタップされた");
        Invoke("changeScene", 1.0f);    // 一秒後にシーンを切り替えるメソッドを呼び出す
    }

    void changeScene()
    {
        if (transform.gameObject.name == "0_EventManager")
        {
            SceneManager.LoadScene("1_Main");   // シーン切り替え
        }
        if (transform.gameObject.name == "1_EventManager")
        {
            SceneManager.LoadScene("2_Result");   // シーン切り替え
        }
        if (transform.gameObject.name == "2_EventManager")
        {
            SceneManager.LoadScene("0_Title");   // シーン切り替え
        }

    }
}
