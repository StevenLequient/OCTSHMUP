using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EnemyPrinter : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float minSpawnInterval = 0.2f;
    public float maxSpawnInterval = 1.5f;
    public float shootingForce = 4f;

    public Rigidbody2D rb;
    public GameObject baseEnemyPrefab;

    private bool goingLeft;
    private Vector2 movement;

    private float nextSpawnTime;


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

        if (Time.fixedTime > nextSpawnTime)
        {
            GameObject enemy = Instantiate(baseEnemyPrefab, transform.position, transform.rotation);
            enemy.transform.SetParent(ShmupController.Instance.transform);
            GameObject actionObject = enemy.transform.GetChild(0).gameObject;

            switch ((int)Random.Range(0, 0.9f))
            {
                default:
                    MoveAction action = actionObject.AddComponent<MoveAction>();
                    action.MoveAmount = 1;
                    MoveAction.MoveDirection direction;
                    switch ((int) Random.Range(0, 3.9f))
                    {
                        case 0:
                            action.Direction = MoveAction.MoveDirection.Up;
                            break;
                        case 1:
                            action.Direction = MoveAction.MoveDirection.Down;
                            break;
                        case 2:
                            action.Direction = MoveAction.MoveDirection.Left;
                            break;
                        default:
                            action.Direction = MoveAction.MoveDirection.Right;
                            break;
                    }
                    break;
            }

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            rb.velocity = -transform.up * shootingForce;

            nextSpawnTime = Time.fixedTime + Random.Range(minSpawnInterval, maxSpawnInterval);
            //rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
    }
}
