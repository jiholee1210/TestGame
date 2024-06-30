using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    private List<Item> items = new List<Item>();

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    
    // 아이템 추가
    public void AddItem(Item item) {
        items.Add(item);
        Debug.Log("아이템 추가 : " + item);
    }

    // 아이템 검색
    public bool HasItem(Item item) {
        return items.Contains(item);
    }

    // ItemType으로 아이템 검색
    public bool HasItem(Item.ItemType itemtype) {
        foreach(Item item in items) {
            if(item.itemType == itemtype) {
                return true;
            }
        }
        return false;
    }
}
