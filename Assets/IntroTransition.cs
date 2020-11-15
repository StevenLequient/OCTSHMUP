using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTransition : MonoBehaviour
{

    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0,speed * Time.deltaTime, 0);
        if (transform.position.y < -1000)
        {
            Destroy(gameObject);
        }
    }
}
