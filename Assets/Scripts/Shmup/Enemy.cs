using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public TetrisAction action;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y < -1f)
        {
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        if (action != null)
        {
            action.Trigger();
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        Debug.Log("Hit Enemy");
        Player playerComponent = hitInfo.collider.GetComponent<Player>();
        if (playerComponent != null)
        {
            playerComponent.Hit();
            Destroy(gameObject);
        }
    }
}
