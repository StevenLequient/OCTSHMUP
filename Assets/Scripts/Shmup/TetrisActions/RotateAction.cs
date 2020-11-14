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

    public RotateDirection Direction;
    public new void Trigger()
    {
        switch (Direction)
        {
            case RotateDirection.Clockwise:
                break;
            case RotateDirection.CounterClockwise:
                break;
            default:
                break;
        }
    }
}
