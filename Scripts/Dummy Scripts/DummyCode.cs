using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DummyCode : MonoBehaviour
{
    private Rigidbody2D dummyRigidbody;
    
    public Transform groundCheckTransform;
    private bool isGrounded;
    private bool isRunning;
    private bool left, right;
    private bool isDead = false;
    public LayerMask groundCheckLayerMask;
    private Animator dummyAnimator;

    private float health;

    void Start()
    {
        right = true;
        dummyRigidbody = GetComponent<Rigidbody2D>();
        dummyAnimator = GetComponent<Animator>();
        health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGroundedStatus();
    }
    void UpdateGroundedStatus()
    {
        //1
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
        //2
        dummyAnimator.SetBool("isGrounded", isGrounded);
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
    public void TakeDamage (float damage)
    {
        // Deduct the damage from the player's health.
        health = health - damage;
        if (health <= 0)
        {
            isDead = true;
            dummyAnimator.SetBool("isDead", isDead);
            Debug.Log("Dummy Defeated!");
        }
        if (!isDead)
        {
            dummyAnimator.SetTrigger("hit");
            Debug.Log("Dummy Health: " + health);
        }
        // If health is less than zero...
    }
}
