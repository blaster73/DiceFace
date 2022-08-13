using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/New Character", order = 0)]
public class Character : ScriptableObject
{

    public string characterName;
    public int health;
    public PowerDice powerDice;
    public TypeDice typeDice;
    public AbilityCard abilityCard;

}
