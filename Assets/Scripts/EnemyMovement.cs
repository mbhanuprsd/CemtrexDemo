using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int speed = 5;
    public int playerRadius = 5;
    GameObject player;
    GameManager gameManager;
    public int spawnIndex;
    Transform targetPoint;
    MeshRenderer meshRenderer;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private float nextFire;
    private float firerate = 1.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = FindObjectOfType<GameManager>();
        meshRenderer = GetComponent<MeshRenderer>();
        ChangeTarget();
        nextFire = Time.time + firerate;
    }

    void Update()
    {
        float step = Time.deltaTime * speed;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < playerRadius)
        {
            AttackMovement(step);
        }
        else
        {
            MoveRandom(step);
        }
    }

    private void AttackMovement(float step)
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        StartCoroutine(ShotEffect());
        if (Time.time > nextFire)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(10);
            nextFire = Time.time + firerate;
        }
    }

    private void MoveRandom(float step)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);
        if (transform.position == targetPoint.transform.position)
        {
            ChangeTarget();
        }
    }

    private void ChangeTarget()
    {
        spawnIndex = spawnIndex < 7 ? spawnIndex + 1 : 0;
        targetPoint = gameManager.spawnPoints[spawnIndex];
    }

    private IEnumerator ShotEffect()
    {
        Color colorEnemy = meshRenderer.material.color;
        colorEnemy.a = 1.0f;
        meshRenderer.material.color = colorEnemy;

        yield return shotDuration;

        colorEnemy.a = 0.5f;
        meshRenderer.material.color = colorEnemy;
    }
}
