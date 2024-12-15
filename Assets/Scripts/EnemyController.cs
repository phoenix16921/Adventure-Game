using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Rigidbody2D rigidbody2d;
    //Enemies speed
    protected float enemySpeed;

    protected bool isForth;
    protected bool isBack;
    protected bool isUp;
    protected bool isDown;
    protected float cooldownTime;
    protected float currentTime;

    //The damage when colliding with the enemies
    protected int collideValue;

    protected Animator animator;
    protected bool isAgressive;
    // Start is called before the first frame update
    protected void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        enemySpeed = 3.5f;

        isForth = true;
        isBack = false;
        isUp = false;
        isDown = false;

        cooldownTime = 3.0f;
        currentTime = cooldownTime;

        collideValue = -2;

        animator = GetComponent<Animator>();
        isAgressive = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

    }
}
