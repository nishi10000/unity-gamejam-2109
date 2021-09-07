using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTextureColor : MonoBehaviour
{
    //sourceTexのサイズから判断し、カラーデータを取ってくるようにする。
    //sourceRectの廃止。
    
    public Texture2D sourceTex;

    //public Rect sourceRect;

    [SerializeField]
    CastingParameter castingParameter = null;

    [SerializeField]
    CastingEvent castingAlphaParameterInputEndEvent = null;
    void Start()
    {
        //int x = Mathf.FloorToInt(sourceRect.x);
        int x = 0;
        //int y = Mathf.FloorToInt(sourceRect.y);
        int y = 0;
        //int width = Mathf.FloorToInt(sourceRect.width);
        int width = 1;//幅１のみ必要なので
        //int height = Mathf.FloorToInt(sourceRect.height); 
        int height = Mathf.FloorToInt(sourceTex.height); //画像の高さ分必要なので。

        Color[] pix = sourceTex.GetPixels(x, y, width, height);
        //Texture2D destTex = new Texture2D(width, height);
        //destTex.SetPixels(pix);
        //destTex.Apply();

        // Set the current object's texture to show the
        // extracted rectangle.
        //GetComponent<Renderer>().material.mainTexture = destTex;
        SetCastingParameter(pix);
    }
    /// <summary>
    /// CastingParameterにColorのR（とりあえず）を入れる。
    /// </summary>
    void SetCastingParameter(Color[] pix)
    {
        float[] Colordata = new float[pix.Length];
        for (int i = 0; i < pix.Length; i++)  //カラーの配列からfloatの配列に変換。
        {
            Colordata[i] = pix[i].r;
        }
        castingParameter.CastingAlpha = new List<float>(Colordata);
        castingAlphaParameterInputEndEvent.Raise();  //イベント発火
    }
}
