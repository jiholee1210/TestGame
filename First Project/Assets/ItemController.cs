using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private Animator animator;
    public Item item;
    public Item.ItemType type;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        item = Item.SetItem(type);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 아이템 획득
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            animator.SetBool("Gotten", true);
            PlayerInventory.Instance.AddItem(item);
            
            // 데미지 업그레이드 아이템 획득 시
            if(item.itemType == Item.ItemType.DmgUpgrade) {
                Player.Instance.attackDmg = 100f;
                Debug.Log("공격력 증가 : " + Player.Instance.attackDmg);
            }
        }
    }
}
