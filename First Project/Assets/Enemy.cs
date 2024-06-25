using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    
    private Rigidbody2D rigidbody;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*public void TakeDamage(float damage) {
        Hp -= 1;
        Debug.Log("적 체력 : " + Hp);

        Vector2 damagedPos = playerPos.position.x > transform.position.x ? new Vector2(-5, 0) : new Vector2(5, 0);
        Debug.Log(damagedPos);

        
        rigidbody.AddForce(damagedPos, ForceMode2D.Impulse);
    }*/

    
    
}
