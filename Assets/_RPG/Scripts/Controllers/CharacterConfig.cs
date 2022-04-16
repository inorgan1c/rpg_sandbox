using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/New Character")]
public class CharacterConfig : ScriptableObject
{
    public enum CharacterClassType
    {
        Skeleton,
        Deer,
        Soldier
    }

    public CharacterClassType charClass;
    public int xp;
    public List<Item> loot;
    public float runMultiplier;
    public float patrolAreaRadius;
    public float maxWaitPeriod;
}
