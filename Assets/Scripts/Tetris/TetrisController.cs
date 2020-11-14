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

    public GameObject blockPrefab;
    
    public int boardWidth = 10;
    public int boardHeight = 30;

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
        grid = new Transform[boardWidth,boardHeight];
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
        if (movingTetromino != null)
        {
            movingTetromino.MoveLeft();
        }
    }

    public void MoveRight()
    {
        if (movingTetromino != null)
        {
            movingTetromino.MoveRight();
        }
    }

    public void MoveDown()
    {
        if (movingTetromino != null)
        {
            movingTetromino.MoveDown();
            previousFallTime = Time.time;
        }
    }

    public void SlamDown()
    {
        if (movingTetromino != null)
        {
            movingTetromino.SlamDown();
        }
    }

    public void RotateCCW()
    {
        if (movingTetromino != null)
        {
            movingTetromino.RotateCCW();
        }
    }

    public void RotateCW()
    {
        if (movingTetromino != null)
        {
            movingTetromino.RotateCW();
        }
    }

    public void Damage()
    {
        if (movingTetromino != null)
        {
            MoveDown();
            MoveUpAllLines();
            movingTetromino.transform.position += new Vector3(0, 1, 0);
            AddBottomLine();
        }
    }

    private void AddBottomLine()
    {
        int randomX = Random.Range(0, boardWidth);
        for (int x = 0; x < boardWidth; x++)
        {
            if (x != randomX)
            {
                GameObject block = Instantiate(blockPrefab, transform.position + new Vector3(x, 0, 0), Quaternion.identity);
                grid[x, 0] = block.transform;
            }
        }
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
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    MoveRight();
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    MoveDown();
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    SlamDown();
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    RotateCCW();
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    RotateCW();
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Damage();
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
    
    private void MoveUpAllLines()
    {
        {
            int y = boardHeight - 1;
            for (int x = 0; x < boardWidth; x++)
            {
                if (grid[x, y] != null)
                {
                    Destroy(grid[x,y].gameObject);
                    grid[x, y] = null;
                }
            }
        }
        
        for (int y = boardHeight - 2; y >= 0; y--)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y + 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y + 1].transform.position += new Vector3(0,1,0);
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
