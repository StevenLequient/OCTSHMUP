using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TetrisController : MonoBehaviour
{
    private static TetrisController _instance;
    public static TetrisController Instance => _instance;
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    public bool directControl;
    private Tetromino movingTetromino;
    
    public float fallTimeInterval = 0.8f;
    private float previousFallTime;

    public Vector3 spawnPoint;
    public GameObject[] piecesToSpawn;
    private List<int> randomPieceBag = new List<int>();
    
    public int boardWidth = 10;
    public int boardHeight = 20;

    public Transform[,] grid;

    public bool dead = false;
    public int clearedLines = 0;

    public GameObject lineClearEffectPrefab;
    public float lineClearEffectDuration = 0.5f;
    private List<int> linesToClear = new List<int>();
    private List<GameObject> lineClearEffects = new List<GameObject>();
    private float lineTime;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Transform[boardWidth,boardHeight + 10];
        SpawnPiece();
    }
    
    void SpawnPiece()
    {
        if (randomPieceBag.Count == 0)
        {
            InitRandomBag();
        }

        int index = Random.Range(0, randomPieceBag.Count);
        int selectedPiece = randomPieceBag[index];
        randomPieceBag.RemoveAt(index);
        
        if (randomPieceBag.Count == 0)
        {
            InitRandomBag();
        }
        
        movingTetromino = Instantiate(piecesToSpawn[selectedPiece], transform.position + spawnPoint, Quaternion.identity).GetComponent<Tetromino>();
        if (!movingTetromino.ValidPosition())
        {
            dead = true;
            movingTetromino = null;
            Debug.Log("You lose!");
        }
    }

    private void InitRandomBag()
    {
        randomPieceBag.Clear();
        for (int i = 0; i < piecesToSpawn.Length; i++)
        {
            randomPieceBag.Add(i);
        }
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
                movingTetromino = null;
                int linesCleared = ClearLines();
                if (linesCleared == 0)
                {
                    SpawnPiece();
                }

                clearedLines += linesCleared;
            }
        }

        if (lineClearEffects.Count > 0)
        {
            if (Time.time - lineTime > lineClearEffectDuration)
            {
                foreach (GameObject effect in lineClearEffects)
                {
                    Destroy(effect);
                }
                lineClearEffects.Clear();

                foreach (int y in linesToClear)
                {
                    DeleteLine(y);
                    MoveDownLinesAbove(y);
                }
                linesToClear.Clear();
                SpawnPiece();
            }
        }
    }

    private int ClearLines()
    {
        int cleared_lines = 0;
        linesToClear.Clear();
        for (int y = boardHeight - 1; y >= 0; y--)
        {
            if (CheckLine(y))
            {
                cleared_lines += 1;
                linesToClear.Add(y);
                lineClearEffects.Add(Instantiate(lineClearEffectPrefab, transform.position + new Vector3(4.5f, y, 0), Quaternion.identity));
            }
        }

        if (cleared_lines > 0)
        {
            lineTime = Time.time;
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
