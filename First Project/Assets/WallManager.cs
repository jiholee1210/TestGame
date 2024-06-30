using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public GameObject Wall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 플레이어 감지 및 인벤토리 검색
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && PlayerInventory.Instance.HasItem(Item.ItemType.Key)) {
            Wall.SetActive(false);
        }
    }
}
