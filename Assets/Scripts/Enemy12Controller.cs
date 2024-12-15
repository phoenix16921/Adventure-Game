using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy12Controller : EnemyController
{
    float vendingMachineSpeed;
    float robotSpeed;
    int collideVendingMachine;
    int collideRobot;

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        vendingMachineSpeed = 1.0f;
        robotSpeed = 2.0f;
        collideVendingMachine = -1;
        collideRobot = -3;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        currentTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (!isAgressive) return;

        Vector2 position = rigidbody2d.position;
        int valueSpeed = 0;

        if (isForth || isUp) valueSpeed = 1;
        else valueSpeed = -1;

        if (gameObject.CompareTag("Enemy1")) enemySpeed = robotSpeed;
        else if (gameObject.CompareTag("Enemy2")) enemySpeed = vendingMachineSpeed;
        //else enemySpeed = boilerSpeed;

        if (isBack || isForth)
        {
            position.x = rigidbody2d.position.x + valueSpeed * enemySpeed * Time.fixedDeltaTime;
            animator.SetFloat("Move X", valueSpeed);
            animator.SetFloat("Move Y", 0);
        }
        else
        {
            position.y = rigidbody2d.position.y + valueSpeed * enemySpeed * Time.fixedDeltaTime;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", valueSpeed);
        }

        rigidbody2d.MovePosition(position);
    }

    void Move()
    {
        if (currentTime < 0)
        {
            if (isForth)
            {
                isForth = false; isDown = true;
            }
            else if (isDown)
            {
                isDown = false; isBack = true;
            }
            else if (isBack)
            {
                isBack = false; isUp = true;
            }
            else if (isUp)
            {
                isUp = false; isForth = true;
            }
            currentTime = cooldownTime;
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        //int collideValue = 0;
        if (player != null)
        {
            if (gameObject.CompareTag("Enemy1")) collideValue = collideRobot;
            else if (gameObject.CompareTag("Enemy2")) collideValue = collideVendingMachine;
            player.ChangeHealth(collideValue, "Enemy");
        }
    }

    public void BeFixed()
    {
        isAgressive = true;
        rigidbody2d.simulated = false; //discard temporarily the physic system of enemies
    }
}
