using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public Transform playerPos;
    public GameObject keyPrefab;

    private int moveDir = 0;
    private int enemyFace = 0;

    private float maxHp = 1000;
    private float Hp = 1000;
    
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

        Vector3 damagedPos = playerPos.position.x > transform.position.x ? new Vector3(-0.3f, 0, 0) : new Vector3(0.3f, 0, 0);
        Debug.Log(damagedPos);

        transform.position += damagedPos;

        yield return new WaitForSeconds(0.2f);
        isDamaged = false;
    }

    void Die() {
        if(Hp <= 0f) {
            Instantiate(keyPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
