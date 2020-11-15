using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShmupController : MonoBehaviour
{
    private static ShmupController _instance;
    public static ShmupController Instance => _instance;

    // Enemies don't spawn in margins but player can move there
    public float VerticalMarginSize = 1f;
    public float Width = 7.25f;
    public float Height = 9.5f; 
    public float PlayerSpeed = 3f;
    public Sprite SpriteLeftArrow;
    public Sprite SpriteRightArrow;
    public Sprite SpriteUpArrow;
    public Sprite SpriteDownArrow;
    public Sprite SpriteRotateClockwise;
    public Sprite SpriteRotateCounterClockwise;
    public Sprite SpriteNewLine;

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    void Start()
    {
        Transform backgroundTransform = transform.Find("ShmupBackground");
        backgroundTransform.localScale = new Vector3(Width, Height, backgroundTransform.localScale.z);
        backgroundTransform.localPosition = new Vector3(Width/2, Height/2, 0);
        Transform enemyPrinterTransform = transform.Find("EnemyPrinter");
        enemyPrinterTransform.localPosition = new Vector3(Width/2, Height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
