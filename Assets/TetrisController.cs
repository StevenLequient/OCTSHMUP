using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisController : MonoBehaviour
{
    public Boolean directControl;
    public GameObject movingTetromino;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (directControl)
        {
            var tetromino = movingTetromino.GetComponent<Tetromino>();
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                tetromino.MoveLeft();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                tetromino.MoveRight();
            }
        }
    }
}
