using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public Transform playerPos;

    private int moveDir = 0;
    private int enemyFace = 0;

    private float maxHp = 10;
    private float Hp = 10;
    
    private bool isDamaged = false;
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
        Die();
    }

    void Move() { 
        if (!isDamaged) {
            rigidbody.velocity = new Vector2(moveDir * 3f, rigidbody.velocity.y);
        }
    }

    void selectDir() {
        if(!isDamaged) {
            moveDir = Random.Range(-1, 2);
            
            if(moveDir != 0) {
                enemyFace = moveDir;
                animator.SetBool("isWalking", true);
            } else{
                animator.SetBool("isWalking", false);
            }

            spriteRenderer.flipX = enemyFace > 0 ? false : true;
        }
        Invoke("selectDir", 1);
    }

    public IEnumerator TakeDamage(float damage) {
        isDamaged = true;

        Hp -= damage;
        Debug.Log("적 체력 : " + Hp);

        Vector2 damagedPos = playerPos.position.x > transform.position.x ? new Vector2(-3, 0) : new Vector2(3, 0);
        Debug.Log(damagedPos);

        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(damagedPos, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1.5f);
        isDamaged = false;
    }

    void Die() {
        if(Hp <= 0f) {
            Destroy(gameObject);
        }
    }
}
