using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShmupController : MonoBehaviour
{
    private static ShmupController _instance;
    public static ShmupController Instance => _instance;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
