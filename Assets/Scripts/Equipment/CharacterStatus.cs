using UnityEngine;

[CreateAssetMenu(fileName = "HealthStatusData", menuName = "StatusObjects/Health", order = 1)]
public class CharacterStatus : ScriptableObject
{
    public string charName = "name";
    public int level = 1;
    public int baseMaxHp = 0;
    public int baseHp = 0;
    public int baseStr = 0;
    public int maxHp = 0;
    public int hp = 0;
    public int str = 0;
}
