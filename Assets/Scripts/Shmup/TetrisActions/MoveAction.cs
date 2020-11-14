using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveAction : TetrisAction
{
    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public MoveDirection Direction;
    public int MoveAmount;

    public new void Trigger()
    {
        switch (Direction)
        {
            case MoveDirection.Up:
                break;
            case MoveDirection.Down:
                break;
            case MoveDirection.Left:
                break;
            case MoveDirection.Right:
                break;
            default:
                break;
        }
    }
}
