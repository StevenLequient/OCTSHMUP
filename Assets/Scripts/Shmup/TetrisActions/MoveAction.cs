using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class MoveAction : TetrisAction
{
    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    private MoveDirection direction;
    public MoveDirection Direction
    {
        get => direction;
        set
        {
            switch (value)
            {
                case MoveDirection.Up:
                    SetSprite(ShmupController.Instance.SpriteUpArrow);
                    break;
                case MoveDirection.Down:
                    SetSprite(ShmupController.Instance.SpriteDownArrow);
                    break;
                case MoveDirection.Left:
                    SetSprite(ShmupController.Instance.SpriteLeftArrow);
                    break;
                case MoveDirection.Right:
                    SetSprite(ShmupController.Instance.SpriteRightArrow);
                    break;
                default:
                    break;
            }

            direction = value;
        }
    }
    public int MoveAmount;

    void Start()
    {
        
    }

    public override void Trigger()
    {
        switch (direction)
        {
            case MoveDirection.Up:
                break;
            case MoveDirection.Down:
                TetrisController.Instance.SlamDown();
                break;
            case MoveDirection.Left:
                TetrisController.Instance.MoveLeft();
                break;
            case MoveDirection.Right:
                TetrisController.Instance.MoveRight();
                break;
            default:
                break;
        }
    }
}
