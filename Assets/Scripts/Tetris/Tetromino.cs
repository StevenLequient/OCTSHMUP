using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public bool frozen = false;
    public Vector3 rotationPivot;

    private Transform tetrisBoard;

    private TetrisController tetrisController;
    // Start is called before the first frame update
    void Awake()
    {
        tetrisController = FindObjectOfType<TetrisController>();
        tetrisBoard = tetrisController.GetComponent<Transform>();
    }

    public void MoveLeft()
    {
        var move =  new Vector3(-1,0,0);
        transform.position += move;
        if (!ValidPosition())
        {
            transform.position -= move;
        }
    }
    
    public void MoveRight()
    {
        var move =  new Vector3(1,0,0);
        transform.position += move;
        if (!ValidPosition())
        {
            transform.position -= move;
        }
    }

    public void RotateCCW()
    {
        transform.RotateAround(transform.TransformPoint(rotationPivot), new Vector3(0,0,1), 90);
        if (!ValidPosition())
        {
            transform.RotateAround(transform.TransformPoint(rotationPivot), new Vector3(0,0,1), -90);
        }
    }

    public void RotateCW()
    {
        transform.RotateAround(transform.TransformPoint(rotationPivot), new Vector3(0,0,1), -90);
        if (!ValidPosition())
        {
            transform.RotateAround(transform.TransformPoint(rotationPivot), new Vector3(0,0,1), 90);
        }
    }

    public void MoveDown()
    {
        var move =  new Vector3(0,-1,0);
        transform.position += move;
        if (!ValidPosition())
        {
            transform.position -= move;
            frozen = true;
        }
    }

    public bool ValidPosition()
    {
        foreach (Transform children in transform)
        {
            var newPos = tetrisBoard.InverseTransformPoint(children.position);
            int x = Mathf.RoundToInt(newPos.x);
            int y = Mathf.RoundToInt(newPos.y);

            if (x < 0 || x >= 10 || y < 0)
            {
                return false;
            }

            if (tetrisController.grid[x, y] != null)
            {
                return false;
            }
        }

        return true;
    }

    public void SlamDown()
    {
        while (!frozen)
        {
            MoveDown();
        }
    }
}
