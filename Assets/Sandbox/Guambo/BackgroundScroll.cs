using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 5.0f;
    public Transform respawnPosition = null;
    public Transform endPositionTransform = null;
    private Vector3 startPosition = Vector3.zero;
    private Vector3 endPosition = Vector3.zero;

    public List<GameObject> backgrounds;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = respawnPosition.position;
        endPosition = endPositionTransform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveBackgrounds();
    }
    void MoveBackgrounds() {
        foreach (GameObject background in backgrounds) {
            background.transform.Translate(Vector2.up * -1 * scrollSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "background") {
            collision.gameObject.transform.position = startPosition;
        }    
    }
}
