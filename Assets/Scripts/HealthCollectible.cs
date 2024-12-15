using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    int strawberryhealth = 1;
    int coconuthealth = 2;
    int candyhealth = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Object that entered the trigger " + collision);
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null && player.health < player.maxHealth)
        {
            int amount = 0;
            if (gameObject.CompareTag("StrawberryHealth")) amount = strawberryhealth;
            else if (gameObject.CompareTag("CoconutHealth")) amount = coconuthealth;
            else amount = candyhealth;
            player.ChangeHealth(amount, "Collectible");
            Destroy(gameObject);
        }
    }
}
