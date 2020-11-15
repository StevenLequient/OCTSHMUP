using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject impactEffect;

    void Update()
    {
        if (transform.localPosition.y > ShmupController.Instance.Height + 1f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.GetComponent<Player>() != null || hitInfo.GetComponent<Bullet>() != null)
        {
            return;
        }

        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (impactEffect != null)
            {
                Instantiate(impactEffect, transform.position, transform.rotation);
            }

            enemy.Hit();
            Destroy(gameObject);
        }
    }
}
