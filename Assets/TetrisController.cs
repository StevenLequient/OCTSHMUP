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

    public Transform[,] grid = new Transform[10,30];


    void SpawnPiece()
    {
        movingTetromino = Instantiate(piecesToSpawn[Random.Range(0, piecesToSpawn.Length)], transform.position + spawnPoint, Quaternion.identity).GetComponent<Tetromino>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnPiece();
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
                    movingTetromino.MoveLeft();
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    movingTetromino.MoveRight();
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    movingTetromino.MoveDown();
                    previousFallTime = Time.time;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    movingTetromino.SlamDown();
                }
                else if (Input.GetKeyDown(KeyCode.Z))
                {
                    movingTetromino.RotateCCW();
                }
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    movingTetromino.RotateCW();
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
                SpawnPiece();
            }
        }
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
