using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    public string itemName;
    public ItemType itemType;

    public enum ItemType {
        DashItem,
        DmgUpgrade,
        Key
    }

    public Item() {

    }
    
    public Item(string itemName, ItemType itemtype) {
        this.itemName = itemName;
        this.itemType = itemtype;
    }

    // ItemType에 따른 아이템 초기화
    public static Item SetItem(ItemType itemType) {
        Item item = null;

        switch (itemType) {
            case ItemType.DashItem:
                item = new Item("DashItem", ItemType.DashItem);
                break;
            case ItemType.DmgUpgrade:
                item = new Item("DmgUpgrade", ItemType.DmgUpgrade);
                break;
            case ItemType.Key:
                item = new Item("Key", ItemType.Key);  
                break;
        }

        return item;
    }
}
