using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float newPos = startPos + (parallaxEffect * Time.time) % length;
        transform.localPosition = new Vector3(newPos, transform.localPosition.y, transform.localPosition.z);
    }
}
