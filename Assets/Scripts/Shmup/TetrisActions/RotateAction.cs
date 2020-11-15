using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAction : TetrisAction
{
    public enum RotateDirection
    {
        Clockwise,
        CounterClockwise
    }

    public int RotateAmount;

    private RotateDirection direction;
    public RotateDirection Direction
    {
        get => direction;
        set
        {
            switch (value)
            {
                case RotateDirection.Clockwise:
                    SetSprite(ShmupController.Instance.SpriteRotateClockwise);
                    break;
                case RotateDirection.CounterClockwise:
                    SetSprite(ShmupController.Instance.SpriteRotateCounterClockwise);
                    break;
                default:
                    break;
            }

            direction = value;
        }
    }
    public override void Trigger()
    {
        switch (Direction)
        {
            case RotateDirection.Clockwise:
                TetrisController.Instance.RotateCW();
                break;
            case RotateDirection.CounterClockwise:
                TetrisController.Instance.RotateCCW();
                break;
            default:
                break;
        }
    }
}
