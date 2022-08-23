using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability Card", menuName = "Ability Card", order = 2)]
public class AbilityCard : ScriptableObject
{

    public enum Ability {PowReroll, TypeReroll, powDouble};

    // Figure out slots where Ability Cards can be checked in calculated if they need to be

    public Ability ability;

}
