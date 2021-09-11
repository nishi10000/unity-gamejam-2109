using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TextureEditor : MonoBehaviour
{
    //メニュー拡張
    [MenuItem("Assets/InverseTextureColor", false, 1)]
    static void GenerateMaskMap()
    {

        //選択されたもの毎の処理
        foreach (var o in Selection.objects)
        {
            //パス取得
            var obj = (UnityEngine.Object)o;
            if (obj == null) { continue; }
            var path = AssetDatabase.GetAssetPath(obj);

            //テクスチャに変換
            Texture texture = o as Texture;
            if (texture)
            {

                Texture2D oldTexture = MakeMap.ToTexture2D(texture);
                Texture2D newTexture = new Texture2D(oldTexture.width, oldTexture.height);
                Color colorCache = Color.white;
                for (int y = 0; y < oldTexture.height; y++)
                    for (int x = 0; x < oldTexture.width; x++)
                    {
                        //テクスチャのピクセルごとの処理
                        colorCache.r = oldTexture.GetPixel(x, y).r * (-1) + 1;
                        colorCache.g = oldTexture.GetPixel(x, y).g * (-1) + 1;
                        colorCache.b = oldTexture.GetPixel(x, y).b * (-1) + 1;

                        newTexture.SetPixel(x, y, colorCache);
                    }
                newTexture.Apply();

                System.IO.File.WriteAllBytes(path, newTexture.EncodeToPNG());
                DestroyImmediate(texture);

                AssetDatabase.Refresh();
                Debug.Log("Inverse texture color: " + path);


            }

        }

    }
}
