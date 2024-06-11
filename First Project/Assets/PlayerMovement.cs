using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidbody;
    private bool isJumping = false;

    private int jumpCount = 0;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && jumpCount < 2) {
            isJumping = true;
        }   
    }

    void FixedUpdate() {
        Move();
        Jump();
    }

    void Move() {
        float xInput = Input.GetAxisRaw("Horizontal");

        rigidbody.velocity = new Vector2(xInput * 8f, rigidbody.velocity.y);
    }

    void Jump() {
        if(isJumping) {
            Debug.Log("점프");
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
            rigidbody.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
            jumpCount++;
            isJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Platform") {
            jumpCount = 0;
        }
    }
}
