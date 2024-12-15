using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public InputAction MoveAction;
    public InputAction Activities;

    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float moveSpeed;

    public int maxHealth;
    public int health { get { return currentHealth; } }
    int currentHealth;

    //Variables related to temporary invincibility
    public float timeInvincible;
    bool isInvincible;
    float damageCooldown;

    //Healing time interval
    public float healingTime;
    bool isWaitForHealing;
    float healingCooldown;

    //Taking water
    public int maxWater;
    int currentWater;
    int increaseAmountOfWater;
    bool isInWater;

    //Cakes owner
    public int numberOfCake;
    public bool canGetCake;
    public bool cakeIsGotten;

    int decreaseSpeed;

    Animator animatorPlayer;
    Vector2 moveDirection;

    public GameObject projectilePrefab;
    float projectileSpeed;

    //Shield Energy(Child object of player)
    public GameObject shieldEnergy;
    bool isTurnOnShield;
    float shieldCooldown;
    float currentShieldCooldown;
    int shieldDecreaseDmg;

    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;

        MoveAction.Enable();
        Activities.Enable();
        Activities.performed += onActivityPerformed; //register a callback 
        Activities.performed += Launch;
        Activities.performed += TurnOnShield;

        rigidbody2d = GetComponent<Rigidbody2D>();
        moveSpeed = 4.0f;
        maxHealth = 5;
        currentHealth = maxHealth;

        timeInvincible = 2.0f;
        isInvincible = false;
        damageCooldown = 0;

        healingTime = 0.5f;
        isWaitForHealing = false;
        healingCooldown = 0;

        maxWater = 100;
        currentWater = 0;
        increaseAmountOfWater = 25;
        isInWater = false;

        numberOfCake = 0;
        canGetCake = false;
        cakeIsGotten = false;

        decreaseSpeed = 4;

        animatorPlayer = GetComponent<Animator>();
        moveDirection = new Vector2(1, 0); //to save the final player's direction when coming back to idle status

        projectileSpeed = 300.0f;

        shieldEnergy = transform.Find("ShieldEnergy").gameObject;
        shieldEnergy.SetActive(false);
        isTurnOnShield = false;
        shieldCooldown = 5.0f;
        currentShieldCooldown = shieldCooldown;
        shieldDecreaseDmg = 2;
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        //Should not use the Transform property (in the Update function) cause Update func acts like FPS -> not good for processing the collider
        //=> use the FixedUpdate func instead (it sync with the physic system
        //Vector2 position = (Vector2)transform.position + move * 3.0f * Time.deltaTime;
        //transform.position = position;

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0) isInvincible = false;
        }

        if (isWaitForHealing)
        {
            healingCooldown -= Time.deltaTime;
            if (healingCooldown < 0) isWaitForHealing = false;
        }

        //save the final player's direction when coming back to idle status
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        animatorPlayer.SetFloat("Look X", moveDirection.x);
        animatorPlayer.SetFloat("Look Y", moveDirection.y);
        animatorPlayer.SetFloat("Speed", move.magnitude); //nomalized

        if (isTurnOnShield)
        {
            currentShieldCooldown -= Time.deltaTime;
            if (currentShieldCooldown < 0)
            {
                isTurnOnShield = false;
                currentShieldCooldown = shieldCooldown;
                shieldEnergy.SetActive(false);
            }
        }

    }

    void FixedUpdate()
    {
        //Use the FixedUpdate func instead:
        Vector2 position = (Vector2)rigidbody2d.position + move * moveSpeed * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);


    }

    void onActivityPerformed(InputAction.CallbackContext context)
    {
        //Debug.Log($"Control Path: {context.control.path}");
        if (context.control.path == "/Keyboard/z" && isInWater) TakeWater();
        else if (context.control.path == "/Keyboard/x" && canGetCake) GetCake();
    }

    void Launch(InputAction.CallbackContext context)
    {
        if (context.control.path == "/Keyboard/c")
        {
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(moveDirection, projectileSpeed);
            animatorPlayer.SetTrigger("Launch");
        }
    }

    void TurnOnShield(InputAction.CallbackContext context)
    {
        if (isTurnOnShield) return;
        if (context.control.path == "/Keyboard/v")
        {
            isTurnOnShield = true;
            shieldEnergy.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "WaterPlacementTilemap")
        {
            isInWater = true;
            moveSpeed /= decreaseSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "WaterPlacementTilemap")
        {
            isInWater = false;
            moveSpeed *= decreaseSpeed;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{

    //}

    //private void OnCollisionStay2D(Collision2D collision)
    //{

    //}


    public void ChangeHealth(int amount, string reason)
    {
        switch (reason)
        {
            //case "Collectible":
            //    break;
            case "Damage":
                if (isInvincible) return;
                isInvincible = true;
                damageCooldown = timeInvincible;
                if (isTurnOnShield) amount += shieldDecreaseDmg;

                animatorPlayer.SetTrigger("Hit");
                break;
            case "Healing":
                if (isWaitForHealing) return;
                isWaitForHealing = true;
                healingCooldown = healingTime;
                break;
            case "Enemy":
                if (isTurnOnShield) amount += shieldDecreaseDmg;
                animatorPlayer.SetTrigger("Hit");
                break;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHandle.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

    void TakeWater()
    {
        currentWater = Mathf.Clamp(currentWater + increaseAmountOfWater, 0, maxWater);
        UIHandle.instance.SetAmountOfWater(currentWater);
    }

    void GetCake()
    {
        numberOfCake++;
        UIHandle.instance.SetAmountOfCake(numberOfCake);
        cakeIsGotten = true;
    }

    private void OnDestroy()
    {
        Activities.performed -= onActivityPerformed;
        Activities.performed -= Launch;
        Activities.performed -= TurnOnShield;
    }
}
