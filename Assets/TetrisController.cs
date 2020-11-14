using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TetrisController : MonoBehaviour
{
    public bool directControl;
    private Tetromino movingTetromino;
    
    public float fallTimeInterval = 0.8f;
    private float previousFallTime;

    public Vector3 spawnPoint;
    public GameObject[] piecesToSpawn;
    
    public int boardWidth = 10;
    public int boardHeight = 20;

    public Transform[,] grid;


    void SpawnPiece()
    {
        movingTetromino = Instantiate(piecesToSpawn[Random.Range(0, piecesToSpawn.Length)], transform.position + spawnPoint, Quaternion.identity).GetComponent<Tetromino>();
    }
    // Start is called before the first frame update
    void Start()
    {
        grid = new Transform[boardWidth,boardHeight + 10];
        SpawnPiece();
    }

    public void MoveLeft()
    {
        movingTetromino.MoveLeft();
    }

    public void MoveRight()
    {
        movingTetromino.MoveRight();
    }

    public void MoveDown()
    {
        movingTetromino.MoveDown();
        previousFallTime = Time.time;
    }

    public void SlamDown()
    {
        movingTetromino.SlamDown();
    }

    public void RotateCCW()
    {
        movingTetromino.RotateCCW();
    }

    public void RotateCW()
    {
        movingTetromino.RotateCW();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (movingTetromino != null)
        {
            if (directControl)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    MoveLeft();
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    MoveRight();
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    MoveDown();
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    SlamDown();
                }
                else if (Input.GetKeyDown(KeyCode.Z))
                {
                    RotateCCW();
                }
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    RotateCW();
                }
            }

            if (Time.time - previousFallTime > fallTimeInterval)
            {
                movingTetromino.MoveDown();
                previousFallTime = Time.time;
            }

            if (movingTetromino.frozen)
            {
                AddToGrid();
                ClearLines();
                SpawnPiece();
            }
        }
    }

    private int ClearLines()
    {
        int cleared_lines = 0;
        for (int y = boardHeight - 1; y >= 0; y--)
        {
            if (CheckLine(y))
            {
                cleared_lines += 1;
                DeleteLine(y);
                MoveDownLinesAbove(y);
            }
        }

        return cleared_lines;
    }

    private void MoveDownLinesAbove(int deleted_y)
    {
        for (int y = deleted_y + 1; y < boardHeight; y++)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].transform.position -= new Vector3(0,1,0);
                }
            }
        }
    }

    private void DeleteLine(int y)
    {
        for (int x = 0; x < boardWidth; x++)
        {
            Destroy(grid[x,y].gameObject);
            grid[x, y] = null;
        }
    }

    private bool CheckLine(int y)
    {
        for (int x = 0; x < boardWidth; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }

        return true;
    }

    private void AddToGrid()
    {
        foreach (Transform children in movingTetromino.transform)
        {
            var newPos = transform.InverseTransformPoint(children.position);
            int x = Mathf.RoundToInt(newPos.x);
            int y = Mathf.RoundToInt(newPos.y);
            grid[x, y] = children;
        }
    }
}
