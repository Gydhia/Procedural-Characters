using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableType
{
    HealingPotion = 0,
    ManaPotion = 1,

    BuffPotion = 2
}

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable", order = 5)]
public class Consumable : Item
{
    [UnityEngine.Header("Consumable Datas")]
    public ConsumableType ConsumableType;

    [UnityEngine.Header("Value to buff / regenerate")]
    public int value;
}
