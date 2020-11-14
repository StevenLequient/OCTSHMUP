using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TetrisAction : MonoBehaviour
{
    public abstract void Trigger();

    protected void SetSprite(Sprite sprite)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
        }
    }
}
