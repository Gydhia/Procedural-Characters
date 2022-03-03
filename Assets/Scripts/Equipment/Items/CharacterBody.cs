using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyPart
{
    Hairs = 0,
    HeadAllElements = 1,
    Eyebrow = 2,
    FacialHair = 3,
    Torso = 4,
    Arm_Upper_Right = 5,
    Arm_Upper_Left = 6,
    Arm_Lower_Right = 7,
    Arm_Lower_Left = 8,
    Hand_Right = 9,
    Hand_Left = 10,
    Hips = 11,
    Leg_Right = 12,
    Leg_Left = 13,
}
public enum RootBones
{
    Spine_03 = 0,
    Neck = 1,
    Eyebrows = 2,
    Head = 3,
    Hips_R = 4,
    Clavicle_R = 5,
    Clavicle_L = 6,
    Shoulder_R = 7,
    Shoulder_L = 8,
    Hand_R = 9,
    Hand_L = 10,
    // Hips,
    LowerLeg_R = 12,
    LowerLeg_L = 13
}

[CreateAssetMenu(fileName = "new CharacterBody", menuName = "Inventory/CharacterBody", order = 1)]
public class CharacterBody : ScriptableObject
{
    [Header("Body Parts")]
    [EnumNamedArrayAttribute(typeof(BodyPart))]
    public GameObject[] CharBody = new GameObject[Enum.GetNames(typeof(BodyPart)).Length];

    [UnityEngine.Header("Body Colors")]
    public Color SkinColor;

    public Color HairColor;
    public Color FacialHairColor;
}
