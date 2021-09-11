using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingObjectManager : MonoBehaviour
{
    [SerializeField] Sprite baseTexture;
    [SerializeField] Texture2D gradTexture;
    [SerializeField] Color baseColor, castedColor;

    [SerializeField] GetTextureColor gtc;
    [SerializeField] SpriteRenderer spr, sprColor;

    [ContextMenu("SetUp")]
    void Set()
    {
        gtc.sourceTex = gradTexture;
        spr.sprite = baseTexture;
        spr.color = baseColor;
        sprColor.sprite = baseTexture;
        sprColor.color = castedColor;
    }

    private void Awake()
    {
        Set();
    }
}
