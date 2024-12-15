using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    Rigidbody2D rb;
    bool isForth;
    bool isBack;
    bool isUp;
    //bool isDown;
    float maxCooldown;
    float minCooldown;
    float currentTime;
    float npcSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isForth = true; isBack = false; isUp = false;
        //isDown = false;
        maxCooldown = 3.5f;
        minCooldown = 0.5f;
        currentTime = Random.Range(minCooldown, maxCooldown);
        npcSpeed = 4.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime < 0)
        {
            RandomStep();
            currentTime = Random.Range(minCooldown, maxCooldown);
        }
        currentTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        int valueDir = 1;
        Vector2 position = rb.position;
        if (isForth || isUp) valueDir = 1;
        else valueDir = -1;

        if (isForth || isBack)
        {
            position.x = rb.position.x + valueDir * npcSpeed * Time.fixedDeltaTime;
        } else
        {
            position.y = rb.position.y + valueDir * npcSpeed * Time.fixedDeltaTime;
        }

        rb.MovePosition(position);
    }

    void RandomStep()
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
}
