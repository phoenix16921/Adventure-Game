using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    int healingAmount = 1;
    bool isHealing = true;
    public bool ishealing { get { return isHealing; } }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(healingAmount, "Healing");
        }
    }
}
