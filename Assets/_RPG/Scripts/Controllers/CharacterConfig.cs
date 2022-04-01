using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/New Character")]
public class CharacterConfig : ScriptableObject
{
    public enum CharacterClassType
    {
        Skeleton,
        Soldier
    }

    public CharacterClassType charClass;
    public int xp;
    public List<Item> loot;
}
