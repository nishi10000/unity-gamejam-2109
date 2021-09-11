using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MGMM : EditorWindow
{
    [MenuItem("Window/MetallicGrossMapMaker")]
    static void ShowWindow()
    {
        // ウィンドウ表示
        EditorWindow.GetWindow<MGMM>();
    }

    private Texture2D _gross;
    private Texture2D _metallic;
    private float grossMin = 0, grossMax = 1;
    private float metallicVal = 0;
    private float metallicMin = 0, metallicMax = 1;
    bool sizeCheck = true;
    string lastFilePath = null;
    void OnGUI()
    {
        //入力
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            using(var grossCheck = new EditorGUI.ChangeCheckScope())
            {
                _gross = (Texture2D)EditorGUILayout.ObjectField(new GUIContent("SmoothnessMap", "Smoothnessのテクスチャマップを入れてください。"), _gross, typeof(Texture2D), false);
                if (grossCheck.changed)
                {
                    //誤った上書きを防止するため、SmoothnessMapを変更したときは上書き保存を出来なくする
                    //新規テクスチャ生成から手動で上書きすることは可能
                    lastFilePath = null;
                }
            }
            EditorGUILayout.MinMaxSlider(new GUIContent("SmoothnessRange", "Smoothnessのテクスチャマップが示す値の範囲を指定してください。"), ref grossMin, ref grossMax, 0, 1);
            _metallic = (Texture2D)EditorGUILayout.ObjectField(new GUIContent("MetallicMap", "(任意)Metallicのテクスチャマップを入れてください。"), _metallic, typeof(Texture2D), false);
            if (_metallic)
            {
                EditorGUILayout.MinMaxSlider(new GUIContent("MetallicRange", "Metallicのテクスチャマップが示す値の範囲を指定してください。"), ref metallicMin, ref metallicMax, 0, 1);
            }
            else
            {
                metallicVal = EditorGUILayout.Slider(new GUIContent("Metallic", "Metallicの値を指定してください。"), metallicVal, 0, 1);
            }


            if (check.changed)
            {
                if (_gross && _metallic)
                {
                    Vector2Int smoothnessSize, metallicSize;
                    smoothnessSize = new Vector2Int(_gross.width, _gross.height);
                    metallicSize = new Vector2Int(_metallic.width, _metallic.height);

                    if (smoothnessSize != metallicSize)
                    {
                        //サイズ不一致
                        sizeCheck = false;
                    }
                    else
                    {
                        sizeCheck = true;
                    }
                }
                else
                {
                    sizeCheck = true;
                }
            }
        }

        //errorメッセージ
        if (!sizeCheck)
        {
            EditorGUILayout.HelpBox("SmoothnessMapとMetallicMapのサイズが異なります。同じ解像度にそろえてください。", MessageType.Error);
        }
        if (!_gross)
        {
            EditorGUILayout.HelpBox("SmoothnessMapをセットしてください。", MessageType.Error);
        }

        //出力
        //errorのときはボタンを押せなくする
        EditorGUI.BeginDisabledGroup(!_gross || !sizeCheck);
        if (GUILayout.Button(new GUIContent("新規テクスチャ生成", "テクスチャを生成します。")))
        {
            //デフォルトのファイル名を生成
            string smoothnessMapPass = AssetDatabase.GetAssetPath(_gross);
            string smoothnessMapName = System.IO.Path.GetFileNameWithoutExtension(smoothnessMapPass);

            //保存先のファイルパスを取得
            string filePath = EditorUtility.SaveFilePanel("Save", "Assets", smoothnessMapName + "_MG", "png");

            if (!string.IsNullOrEmpty(filePath))
            {
                //計算
                Texture2D tex;
                if (_metallic)
                {
                    tex = MakeMap.GenerateMGMap_MMap(new Vector2Int(_gross.width, _gross.height), _gross, _metallic, grossMin, grossMax, metallicMin, metallicMax);
                }
                else
                {
                    tex = MakeMap.GenerateMGMap_MVal(new Vector2Int(_gross.width, _gross.height), _gross, grossMin, grossMax, metallicVal);
                }
                
                // 保存処理
                System.IO.File.WriteAllBytes(filePath, tex.EncodeToPNG());
                DestroyImmediate(tex);

                AssetDatabase.Refresh();
                lastFilePath = filePath;
            }
        }

        if (!string.IsNullOrEmpty(lastFilePath))
        {
            if (GUILayout.Button(new GUIContent("上書き保存", "前回生成したテクスチャに上書きします。")))
            {
                //計算
                Texture2D tex;
                if (_metallic)
                {
                    tex = MakeMap.GenerateMGMap_MMap(new Vector2Int(_gross.width, _gross.height), _gross, _metallic, grossMin, grossMax, metallicMin, metallicMax);
                }
                else
                {
                    tex = MakeMap.GenerateMGMap_MVal(new Vector2Int(_gross.width, _gross.height), _gross, grossMin, grossMax, metallicVal);
                }

                // 保存処理
                System.IO.File.WriteAllBytes(lastFilePath, tex.EncodeToPNG());
                DestroyImmediate(tex);

                AssetDatabase.Refresh();
            }
        }
    }

    
}

public static class MakeMap
{
    public static Texture2D GenerateMGMap_MMap(Vector2Int size, Texture gross, Texture metallic, float grossMin = 0, float grossMax = 1,  float metallicMin = 0, float metallicMax = 1)
    {
        Texture2D texture = new Texture2D(size.x, size.y);
        Color colorCache = Color.white;
        Texture2D metallic2D = ToTexture2D(metallic);
        Texture2D gross2D = ToTexture2D(gross);

        for (int y = 0; y < size.y; y++)
            for (int x = 0; x < size.x; x++)
            {
                //テクスチャのピクセルごとの処理
                //metallic
                colorCache.r = metallic2D.GetPixel(x, y).r;
                //最小値と最大値を適用
                colorCache.r = Mathf.Lerp(metallicMin, metallicMax, colorCache.r);

                //gross
                colorCache.a = gross2D.GetPixel(x, y).r;
                //最小値と最大値を適用
                colorCache.a = Mathf.Lerp(grossMin, grossMax, colorCache.a);

                texture.SetPixel(x, y, colorCache);
            }
        texture.Apply();

        return texture;
    }

    public static Texture2D GenerateMGMap_MVal(Vector2Int size, Texture gross, float grossMin = 0, float grossMax = 1, float metallicVal = 0)
    {
        Texture2D texture = new Texture2D(size.x, size.y);
        Color colorCache = Color.white;
        Texture2D gross2D = ToTexture2D(gross);

        for (int y = 0; y < size.y; y++)
            for (int x = 0; x < size.x; x++)
            {
                //テクスチャのピクセルごとの処理
                //metallic
                colorCache.r = metallicVal;

                //gross
                colorCache.a = gross2D.GetPixel(x, y).r;
                //最小値と最大値を適用
                colorCache.a = Mathf.Lerp(grossMin, grossMax, colorCache.a);

                texture.SetPixel(x, y, colorCache);
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
