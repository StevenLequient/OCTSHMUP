using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLineAction : TetrisAction
{
    public int NewAmount;

    void Start()
    {
        SetSprite(ShmupController.Instance.SpriteNewLine);
    }
    public override void Trigger()
    {
        TetrisController.Instance.Damage();
    }
}
