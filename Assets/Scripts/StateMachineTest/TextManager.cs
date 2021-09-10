using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//イベントを受けて、ゲーム画面にScoreを表示する。
public class TextManager : MonoBehaviour
{
    [SerializeField]
    private Text NowScoreText = null;

    [SerializeField]
    private Score score = null;

    public void NowScoreView()
    {
        //最後のスコアを表示する。
        NowScoreText.text = "今の鋳物の得点は"+score.RoundScore[score.RoundScore.Count - 1].ToString()+"だよ！";
    }
   
}
