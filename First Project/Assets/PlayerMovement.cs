using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private bool isDamaged = false;
    private float damagedTime = 0.2f;
    private float invincibility = 0.7f;

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

    // 플레이어 좌우 움직임
    void Move() {
        if(!isDashing && !isDamaged) {
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

    // 플레이어 점프
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
    // 플레이어 공격
    IEnumerator Attack() {
        if(isAttacking) {
            canAttack = false;
            animator.SetTrigger("Attack");
            isAttacking = false;
            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }
    }
    // 플레이어 대쉬
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

    // 플레이어 바닥 감지
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("충돌 : " + other.tag);
        if(other.tag == "Platform") {
            jumpCount = 0;
            animator.SetBool("isJumping", false);
        }
    }
    
    // 플레이어 피격 감지
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy") {
            Debug.Log("콜라이더 충돌" + other.gameObject.tag);
            isDamaged = true;
            StartCoroutine(onDamaged(other.transform.position));
            
        }    
    }

    // 플레이어 피격 애니메이션 및 기능
    IEnumerator onDamaged(Vector2 targetPos) {
        gameObject.layer = 8;
        animator.SetTrigger("Hurt");
        float dir = transform.position.x - targetPos.x >= 0 ? 1f : -1f;
        Debug.Log(dir);
        rigidbody.AddForce(new Vector2(dir, 0f) * 3f , ForceMode2D.Impulse);
        yield return new WaitForSeconds(damagedTime);
        isDamaged = false;
        yield return new WaitForSeconds(invincibility);
        gameObject.layer = 7;
    }
}
