using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TypeDice", menuName = "Dice/TypeDice", order = 1)]
public class TypeDice : ScriptableObject
{

    public enum Type {Magic, Fire, Radiation, Eldritch};
    public Type[] sideTypes;

    public string RollDie()
    {

        Type type = sideTypes[Random.Range(0, sideTypes.Length)];
        string typeString = "";

        switch (type)
        {
            case Type.Magic:
                typeString = "Magic";
                break;
            case Type.Fire:
                typeString = "Fire";
                break;
            case Type.Radiation:
                typeString = "Radiation";
                break;
            case Type.Eldritch:
                typeString = "Eldritch";
                break;
        }

        return typeString;
    }

}
