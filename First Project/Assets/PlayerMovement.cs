using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidbody;
    private bool isJumping = false;

    private int jumpCount = 0;

    private bool isDashing = false;
    private bool canDash = true;
    private float dashCooldown = 0.5f;
    private float dashTime = 0.2f;
    private float playerFace = 1f;

    private bool isAttacking = false;
    private bool canAttack = true;
    private float attackCooldown = 1f;

    SpriteRenderer spriteRenderer;
    Animator animator;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();   
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && jumpCount < 2 && !isDashing) {
            isJumping = true;
        }   
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash) {
            isDashing = true;
        }
        if(Input.GetKeyDown(KeyCode.Z) && canAttack) {
            isAttacking = true;
        }
    }

    void FixedUpdate() {
        Move();
        Jump();
        StartCoroutine(Attack());
        StartCoroutine(Dash());
    }

    void Move() {
        if(!isDashing) {
            float xInput = Input.GetAxisRaw("Horizontal");

            if(xInput != 0) {
                playerFace = xInput;
            }
            spriteRenderer.flipX = playerFace > 0 ? true : false;
            if(xInput == 0) {
                animator.SetInteger("AnimState", 0);
            } else {
                animator.SetInteger("AnimState", 2);
            }
            rigidbody.velocity = new Vector2(xInput * 8f, rigidbody.velocity.y);
        }
    }

    void Jump() {
        if(isJumping) {
            Debug.Log("점프");
            animator.SetBool("isJumping", true);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
            rigidbody.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            jumpCount++;
            isJumping = false;
        }
    }

    IEnumerator Attack() {
        if(isAttacking) {
            canAttack = false;
            
            animator.SetTrigger("Attack");
            isAttacking = false;
            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }
    }
    IEnumerator Dash() {
        if(isDashing) {
            canDash = false;
            rigidbody.gravityScale = 0f;
            rigidbody.velocity = new Vector2(playerFace * 16f, 0f);
            yield return new WaitForSeconds(dashTime);
            rigidbody.gravityScale = 2f;
            isDashing = false;
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("충돌 : " + other.tag);
        if(other.tag == "Platform") {
            jumpCount = 0;
            animator.SetBool("isJumping", false);
        }
    }
}
