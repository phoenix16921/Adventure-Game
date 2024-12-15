using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FOR ENEMY3: BOILER ENEMIES
public class Enemy3Controller : EnemyController
{ 
    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
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

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        //int collideValue = 0;
        if (player != null)
        {
            player.ChangeHealth(collideValue, "Enemy");
            return;
        }
    }

    void Move()
    {
        //Randomly change enemies's direction
        int randDir = Random.Range(0, 4);
        if (randDir == 0)
        {
            isForth = true; isBack = false; isUp = false;
            //isDown = false;
        }
        else if (randDir == 1)
        {
            isForth = false; isBack = true; isUp = false;
            //isDown = false;
        }
        else if (randDir == 2)
        {
            isForth = false; isBack = false; isUp = true;
            //isDown = false;
        }
        else
        {
            isForth = false; isBack = false; isUp = false;
            //isDown = true;
        }
    }

    public void destroyCollideWithProjectile()
    {
        Destroy(gameObject);
    }

    

}
