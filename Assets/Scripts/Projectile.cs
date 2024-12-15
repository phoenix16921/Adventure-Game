using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbodyProjectile;
    float maxProjectileDistance;

    private void Awake()
    {
        rigidbodyProjectile = GetComponent<Rigidbody2D>();  
    }

    private void Start()
    {
        maxProjectileDistance = 80.0f;
    }

    private void Update()
    {
        //If the projectile goes far away from the center of game world -> destroy it
        if (transform.position.magnitude > maxProjectileDistance) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy12Controller enemy = collision.collider.GetComponent<Enemy12Controller>();
        if (enemy != null)
        {
            enemy.BeFixed();
            Destroy(gameObject);
            return;
        }

        Enemy3Controller enemy3 = collision.collider.GetComponent<Enemy3Controller>();
        if (enemy3 != null)
        {
            enemy3.destroyCollideWithProjectile();
            Destroy(gameObject);
            //return;
        }

        //Destroy(gameObject);
    }

    public void Launch(Vector2 direction,float force)
    {
        rigidbodyProjectile.AddForce(direction * force);
    }
}
