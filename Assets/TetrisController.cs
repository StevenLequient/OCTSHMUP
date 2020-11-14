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

    private Transform[,] grid;


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
            var tetromino = movingTetromino.GetComponent<Tetromino>();
            if (directControl)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    tetromino.MoveLeft();
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    tetromino.MoveRight();
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    tetromino.MoveDown();
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    tetromino.SlamDown();
                }
                else if (Input.GetKeyDown(KeyCode.Z))
                {
                    tetromino.RotateCCW();
                }
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    tetromino.RotateCW();
                }
            }

            if (Time.time - previousFallTime > fallTimeInterval)
            {
                tetromino.MoveDown();
                previousFallTime = Time.time;
            }

            if (tetromino.frozen)
            {
                SpawnPiece();
            }
        }
    }
}
