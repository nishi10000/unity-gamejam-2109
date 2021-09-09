using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//鋳型を生成・動かすクラス。各イベントで、生成と動作を行う。
public class CastingMoving : MonoBehaviour
{
    private GameObject InstanceGameObject;
    //鋳型を生成し、移動する。
    public void CastingCreate()
    {
        GameObject obj = GameFSM.instance.CastingGameObjects[0];//TODO:ここはランダムでいいかもしれない。

        // プレハブを元にオブジェクトを生成する
        InstanceGameObject = (GameObject)Instantiate(obj,
                                                      new Vector3(5.0f, 0.0f, 0.0f),
                                                      Quaternion.identity);
        InstanceGameObject.transform.DOMove(new Vector3(0f, 0f, 0f), 1f);
    }
    //鋳型を移動し削除する。
    public void CastingDelete()
    {
        InstanceGameObject.transform.DOMove(new Vector3(-5f, 0f, 0f), 1f).OnComplete(() =>
        {
            Destroy(InstanceGameObject);
        });
    }
}
