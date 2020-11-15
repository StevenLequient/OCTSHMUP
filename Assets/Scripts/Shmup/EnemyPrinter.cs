using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EnemyPrinter : MonoBehaviour
{
    public float minMoveSpeed = 0.5f;
    public float maxMoveSpeed = 4f;
    public float minSpawnInterval = 0.01f;
    public float maxSpawnInterval = 0.02f;

    public Rigidbody2D rb;
    public GameObject baseEnemyPrefab;

    private bool goingLeft;
    private Vector2 movement;

    private float nextSpawnTime;

    void Update()
    {
        ShmupController shmup = ShmupController.Instance;
        float spawnX = Random.Range(shmup.VerticalMarginSize, shmup.Width - shmup.VerticalMarginSize);
        if (Time.fixedTime >= nextSpawnTime)
        {
            GameObject enemy = Instantiate(baseEnemyPrefab, shmup.transform.position + new Vector3(spawnX, shmup.Height, 0), transform.rotation);
            enemy.transform.SetParent(ShmupController.Instance.transform);
            GameObject actionObject = enemy.transform.GetChild(0).gameObject;

            float rand = Random.Range(0, 100);
            if (rand <= 72f)
            {
                MoveAction action = actionObject.AddComponent<MoveAction>();
                action.MoveAmount = 1;
                float directionRand = Random.Range(0, 100);
                if (directionRand <= 45f)
                {
                    action.Direction = MoveAction.MoveDirection.Left;
                }
                else if (directionRand <= 90f)
                {
                    action.Direction = MoveAction.MoveDirection.Right;
                }
                else
                {
                    action.Direction = MoveAction.MoveDirection.Down;
                }
            }
            else if (rand <= 96f)
            {
                RotateAction action = actionObject.AddComponent<RotateAction>();
                switch ((int)Random.Range(0, 1.99f))
                {
                    case 0:
                        action.Direction = RotateAction.RotateDirection.Clockwise;
                        break;
                    default:
                        action.Direction = RotateAction.RotateDirection.CounterClockwise;
                        break;
                }
            }
            else
            {
                NewLineAction action = actionObject.AddComponent<NewLineAction>();
            }

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            rb.velocity = -transform.up * Random.Range(minMoveSpeed, maxMoveSpeed);

            nextSpawnTime = Time.fixedTime + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }
}
