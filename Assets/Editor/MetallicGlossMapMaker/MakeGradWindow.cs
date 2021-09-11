using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MGW : EditorWindow
{
    [MenuItem("Window/GradMaker")]
    static void ShowWindow()
    {
        // ウィンドウ表示
        EditorWindow.GetWindow<MGW>();
    }

    private Texture2D _base;

    void OnGUI()
    {
        //入力
        _base = (Texture2D)EditorGUILayout.ObjectField(new GUIContent("Base", "元画像"), _base, typeof(Texture2D), false);
       

        //出力
        if (GUILayout.Button(new GUIContent("新規テクスチャ生成", "テクスチャを生成します。")))
        {
            //デフォルトのファイル名を生成
            string basePass = AssetDatabase.GetAssetPath(_base);
            string baseName = System.IO.Path.GetFileNameWithoutExtension(basePass);

            //保存先のファイルパスを取得
            string filePath = EditorUtility.SaveFilePanel("Save", "Assets", baseName + "_", "png");

            if (!string.IsNullOrEmpty(filePath))
            {
                //計算
                Texture2D tex = MakeGrad.Generate(_base);
               
                
                // 保存処理
                System.IO.File.WriteAllBytes(filePath, tex.EncodeToPNG());
                DestroyImmediate(tex);

                AssetDatabase.Refresh();
            }
        }
    }

    
}

public static class MakeGrad
{
    public static Texture2D Generate(Texture baseTex)
    {
        Texture2D texture = new Texture2D(baseTex.width, baseTex.height);
        Color colorCache = Color.white;
        Texture2D base2D = ToTexture2D(baseTex);

        for (int y = 0; y < baseTex.height; y++)
        {
            float red = 0;
            for (int x = 0; x < baseTex.width; x++)
            {
                //テクスチャのピクセルごとの処理
                red += base2D.GetPixel(x, y).a;
            }
            red /= baseTex.width;
            red = 1f - red;
            if(red != 1f)
            {
                red = Mathf.Lerp(red, 0.5f, 0.9f);
            }
            if(y == 0 || y == baseTex.height - 1)
            {
                red = 1f;
            }
            for (int x = 0; x < baseTex.width; x++)
            {
                //テクスチャのピクセルごとの処理
                colorCache.r = red;
                colorCache.g = red;
                colorCache.b = red;

                texture.SetPixel(x, y, colorCache);
            }
        }

        texture.Apply();

        return texture;
    }

    public static Texture2D ToTexture2D(this Texture self)
    {
        var sw = self.width;
        var sh = self.height;
        var format = TextureFormat.RGBA32;
        var result = new Texture2D(sw, sh, format, false);
        var currentRT = RenderTexture.active;
        var rt = new RenderTexture(sw, sh, 32);
        Graphics.Blit(self, rt);
        RenderTexture.active = rt;
        var source = new Rect(0, 0, rt.width, rt.height);
        result.ReadPixels(source, 0, 0);
        result.Apply();
        RenderTexture.active = currentRT;
        return result;
    }
}
