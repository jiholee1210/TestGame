using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private int moveDir = 0;
    private int enemyFace = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Invoke("selectDir", 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() {
        Move();
    }

    void Move() { 
        rigidbody.velocity = new Vector2(moveDir * 3f, rigidbody.velocity.y);
    }

    void selectDir() {
        moveDir = Random.Range(-1, 2);
        
        if(moveDir != 0) {
            enemyFace = moveDir;
            animator.SetBool("isWalking", true);
        } else{
            animator.SetBool("isWalking", false);
        }

        spriteRenderer.flipX = enemyFace > 0 ? false : true;

        Invoke("selectDir", 1);
    }
}
