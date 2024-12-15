using System.Collections;
using System.Collections.Generic;
using Unity.XR.Oculus.Input;
using UnityEngine;

public class Cakes : MonoBehaviour
{
    PlayerController player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        player = collision.GetComponent<PlayerController>();
        if (collision != null && collision.name == "PlayerCharacter")
        {
            player.canGetCake = true;
            if (player.cakeIsGotten)
            {
                player.cakeIsGotten = false;
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.name == "PlayerCharacter")
        {
            player.canGetCake = false;
        }
    }
}
