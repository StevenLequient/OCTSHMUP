using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public bool stopped = false;

    private Transform tetrisBoard;
    // Start is called before the first frame update
    void Start()
    {
        tetrisBoard = FindObjectOfType<TetrisController>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveLeft()
    {
        var move =  new Vector3(-1,0,0);
        transform.position += move;
        if (!ValidMove())
        {
            transform.position -= move;
        }
    }
    
    public void MoveRight()
    {
        var move =  new Vector3(1,0,0);
        transform.position += move;
        if (!ValidMove())
        {
            transform.position -= move;
        }
    }

    public void RotateCCW()
    {
        transform.RotateAround(transform.position, new Vector3(0,0,1), 90);
        if (!ValidMove())
        {
            transform.RotateAround(transform.position, new Vector3(0,0,1), -90);
        }
    }

    public void RotateCW()
    {
        transform.RotateAround(transform.position, new Vector3(0,0,1), -90);
        if (!ValidMove())
        {
            transform.RotateAround(transform.position, new Vector3(0,0,1), 90);
        }
    }

    public void MoveDown()
    {
        var move =  new Vector3(0,-1,0);
        transform.position += move;
        if (!ValidMove())
        {
            transform.position -= move;
            stopped = true;
        }
    }

    private bool ValidMove()
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
        }

        return true;
    }
}
