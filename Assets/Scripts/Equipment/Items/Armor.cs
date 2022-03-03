using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ArmorPart
{
    Helmet = 0,
    Chest = 1,
    Arms = 2,
    Hands = 3,
    Legs = 4,
    Boots = 5
}
public enum ArmorType
{
    Light = 0,
    Medium = 1,
    Heavy = 2
}

[CreateAssetMenu(fileName = "New Armor", menuName = "Inventory/Armor", order = 3)]
public class Armor : Item
{
    [UnityEngine.Header("Armor Datas")]
    public ArmorPart ArmorPart;
    public ArmorType ArmorType;
    public GameObject Mesh;

    [UnityEngine.Header("Armor Defensive Stats")]
    public int HpBuff;
    public int PhysDefBuff;
    public int MagDefBuff;

    [UnityEngine.Header("Armor Characteristic Stats")]
    public int strBuff;
    public int dextBuff;
    public int intelBuff;
}
