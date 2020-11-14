using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EnemyPrinter : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float spawnInterval = 3f;
    public float shootingForce = 4f;

    public Rigidbody2D rb;
    public GameObject shmup;
    public GameObject enemyPrefab;

    private bool goingLeft;
    private Vector2 movement;
    private float lastSpawnTime;


    // Update is called once per frame
    void Update()
    {
        float x = moveSpeed;
        if (goingLeft)
        {
            x = -x;
        }

        movement.x = x;
        movement.y = 0;
    }

    void FixedUpdate()
    {
        rb.velocity = movement;

        if (transform.localPosition.x < 0.5f)
        {
            goingLeft = false;
        }

        if (transform.localPosition.x > 6.5f)
        {
            goingLeft = true;
        }

        if (Time.fixedTime - lastSpawnTime > spawnInterval)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
            enemy.transform.SetParent(shmup.transform);
            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            rb.velocity = -transform.up * shootingForce;
            lastSpawnTime = Time.fixedTime;
            //rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
    }
}
