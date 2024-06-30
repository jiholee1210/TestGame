using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public float attackDmg = 1f;
    public float attackCooldown = 0.5f;
    
    public float damagedTime = 0.2f;
    public float invincibility = 0.7f;

    public float dashCooldown = 0.5f;
    public float dashTime = 0.2f;
    
    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
