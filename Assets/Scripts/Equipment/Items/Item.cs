using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { 
    Weapon, 
    Armor, 

    Jewelry,

    Consumable
}
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon;
    public ItemType ItemType;

    public virtual void Use()
    {

    }

    public virtual void Drop()
    {
        // Inventory.Instance.RemoveItem(this);
    }
}
