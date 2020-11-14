using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject impactEffect;

    void Update()
    {
        if (transform.localPosition.y > 15)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.GetComponent<Player>() != null)
        {
            return;
        }
        Enemy enemyComponent = hitInfo.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.Hit();
        }

        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
