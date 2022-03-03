using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    LongSword = 0,
    ShortSword = 1,

    Bow = 2,

    MagicWand = 3
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon", order = 2)]
public class Weapon : Item
{
    [UnityEngine.Header("Weapon Datas")]
    public WeaponType WeaponType;
    public GameObject Mesh;

    [UnityEngine.Header("Weapon stats")]
    public float physDamages;
    public float magDamages;
}
