using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisController : MonoBehaviour
{
    public bool directControl;
    public GameObject movingTetromino;
    
    public float fallTimeInterval = 0.8f;
    private float previousFallTime;
    private Transform[,] grid;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

        if (tetromino.stopped)
        {
            // Spawn another piece
        }
    }
}
