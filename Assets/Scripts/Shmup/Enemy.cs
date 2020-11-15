using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Action;
    public float InvincibleSpawnTime = 0.1f;
    private float spawnTime;

    void Start()
    {
        spawnTime = Time.fixedTime;
    }

    void Update()
    {
        if (transform.localPosition.y < -1f)
        {
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        if (Time.fixedTime < spawnTime + InvincibleSpawnTime)
        {
            return;
        }
        if (Action != null)
        {
            TetrisAction tetrisAction = Action.GetComponent<TetrisAction>();
            if (tetrisAction != null)
            {
                tetrisAction.Trigger();
            }
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if (hitInfo.collider.GetComponent<Player>() != null)
        {
            Destroy(gameObject);
        }
    }
}
