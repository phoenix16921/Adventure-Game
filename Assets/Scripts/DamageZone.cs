using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    PlayerController player;
    int damageZoneValue = 1;
    int damageZoneValue2 = 2;
    int decreaseSpeed = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<PlayerController>();
        if (player != null && player.name == "PlayerCharacter")
        {
            player.moveSpeed /= decreaseSpeed;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //player = collision.GetComponent<PlayerController>();
        if (player != null && player.name == "PlayerCharacter")
        {
            int damageValue = 0;
            if (gameObject.CompareTag("DamageZone")) damageValue = damageZoneValue;
            else damageValue = damageZoneValue2;
            player.ChangeHealth(-damageValue, "Damage");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (player != null && player.name == "PlayerCharacter")
        {
            player.moveSpeed *= decreaseSpeed;
        }
    }
}
