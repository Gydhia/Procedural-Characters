using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JewelryType
{
    Ring = 0,

    Necklace = 1,

    Trinket = 2
}

[CreateAssetMenu(fileName = "New Jewelry", menuName = "Inventory/Jewelry", order = 4)]
public class Jewelry : Item
{
    [UnityEngine.Header("Jewelry Datas")]
    public JewelryType JewelryType;

    [UnityEngine.Header("Jewelry Characteristic Stats")]
    public int strBuff;
    public int dextBuff;
    public int intelBuff;
}
