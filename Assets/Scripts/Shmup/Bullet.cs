using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject impactEffect;

    void Update()
    {
        if (transform.localPosition.y > 9.8f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("Enter");
        if (hitInfo.GetComponent<Player>() != null)
        {
            return;
        }
        Enemy enemyComponent = hitInfo.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            Debug.Log("enemy");
            enemyComponent.Hit();
        }

        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
