using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    [SerializeField]
    Score score;

    [SerializeField]
    Text scoreText;

    private void Awake()
    {
        scoreText.text = score.TotalScore.ToString();    
    }
}
