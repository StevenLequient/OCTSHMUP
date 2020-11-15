using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 5.0f;
    private Vector3 startPosition = Vector3.zero;
    public Transform endPositionTransform = null;
    public Vector3 endPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        endPosition = endPositionTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPosition, scrollSpeed * Time.deltaTime);
        if (transform.position == endPosition) {
            transform.position = startPosition;
        }
    }
}
