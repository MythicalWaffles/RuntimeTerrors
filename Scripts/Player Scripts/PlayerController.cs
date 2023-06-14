using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //Movement
    private Rigidbody2D playerRigidbody;
    public float speed;
    public float jump;
    float moveVelocity;
    
    public Transform groundCheckTransform;
    private bool isGrounded;
    private bool isRunning;
    private bool left, right;
    public LayerMask groundCheckLayerMask;
    private Animator playerAnimator;

    public Image healthBar;
    public float health = 100f;
    public float healthRegen = 5f;
    Vector3 pos;
	StreamReader sr;

    public Image magicBar;
    public float magic = 100f;
    public float magicRegen = 5f;
    public Transform magicSpawn;
    public GameObject fireball;
    public float posOffset;

    public Transform attackPoint;
    public LayerMask enemyLayers;
    
    public float attackRange = 0.5f;
    public int attackDamage = 20;

    public float attackRate = 2f;
    float nextAttackTime = 0f;


    void Start()
    {
        right = true;
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        healthBar.fillAmount = health;
        magicBar.fillAmount = magic;

        health = health + (Time.deltaTime * healthRegen);
        magic = magic + (Time.deltaTime * healthRegen);
        if (health >= 100)
        {
            health = 100;
        }
        if (magic >= 100)
        {
            magic = 100;
        }
//        healthBar.fillAmount = health / 100;
        magicBar.fillAmount = magic / 100;
        //Jumping
        if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.W)) 
        {
            if(isGrounded)
            {
                GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jump);
            }
        }
        
        moveVelocity = 0;

        isRunning = false;
        playerAnimator.SetBool("isRunning", isRunning);

        //Left Right Movement
        if (Input.GetKey (KeyCode.A)) 
        {
            TurnLeft();
            moveVelocity = -speed;
            isRunning = true;
            playerAnimator.SetBool("isRunning", isRunning);
        }
        if (Input.GetKey (KeyCode.D)) 
        {
            TurnRight();
            moveVelocity = speed;
            isRunning = true;
            playerAnimator.SetBool("isRunning", isRunning);
        }
        UpdateGroundedStatus();
        GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);

        //Meele

        if(Time.time >= nextAttackTime)
        {
            if(Input.GetKeyDown(KeyCode.J))
            {
                playerAnimator.SetTrigger("meele");
//                Meele();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        if(Time.time >= nextAttackTime)
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                shoot();
            }
        }
    }

    public void TurnLeft()
    {
        if(left)
            return;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        left = true;
        right = false;
    }

    public void TurnRight()
    {
        if(right)
            return;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        right = true;
        left = false;
    }

    void Meele()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<DummyCode>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void UpdateGroundedStatus()
    {
        //1
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
        //2
        playerAnimator.SetBool("isGrounded", isGrounded);
    }

    public void TakeDamage (float damage)
    {
        // Deduct the damage from the player's health.
        health = health - damage;
        // If health is less than zero...
        if (health < 0)
        {
            // Print a debug message that the game has ended.
            Debug.Log("Game Over!");
            /*Cursor.visible = true;

            // Lock the player's mouse cursor from moving.
            Cursor.lockState = CursorLockMode.None;

            // Freeze the game.
            Time.timeScale = 0;
            SceneManager.LoadScene("Credits");*/
        }
        else
        {
            // Update the health bar to reflect the new player health.
            healthBar.fillAmount = health / 100;
        }
    }

    void shoot()
    {
        if (magic >= 5)
        {
            magic -= 5;
            magicBar.fillAmount = magic / 100;
            if (right) {
                    Vector3 projPosition = new Vector3(magicSpawn.transform.position.x + posOffset, magicSpawn.transform.position.y, magicSpawn.transform.position.z);
                    GameObject g = Instantiate (fireball, projPosition, Quaternion.identity) as GameObject;
                    Destroy(g, 1.5f);
                } else {
                    Vector3 projPosition = new Vector3(magicSpawn.transform.position.x - posOffset, magicSpawn.transform.position.y, magicSpawn.transform.position.z);
                    GameObject g = Instantiate (fireball, projPosition, Quaternion.Euler(Vector3.down * 180f))  as GameObject;
                    Destroy(g, 1.5f);
                }
        }
/*        GameObject g = Instantiate(fireball, magicSpawn.position, magicSpawn.transform.rotation)
               as GameObject;
               Destroy(g, 1.5f);*/
    }
}