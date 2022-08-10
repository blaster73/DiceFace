using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerDice", menuName = "Dice/PowerDice", order = 2)]
public class PowerDice : ScriptableObject
{

    public enum Sides {D4, D6, D8, D10, D20};
    public Sides sides;

    public int RollDie()
    {

        int power = 0;

        switch (sides)
        {
            case Sides.D4:
                power = Random.Range(1, 5);
                //Debug.Log("Rolled power: " + power);
                break;
            case Sides.D6:
                power = Random.Range(1, 7);
                //Debug.Log("Rolled power: " + power);
                break;
            case Sides.D8:
                power = Random.Range(1, 9);
                //Debug.Log("Rolled power: " + power);
                break;
            case Sides.D10:
                power = Random.Range(1, 11);
                //Debug.Log("Rolled power: " + power);
                break;
            case Sides.D20:
                power = Random.Range(1, 21);
                //Debug.Log("Rolled power: " + power);
                break;
            default:
                power = 0;
                //Debug.Log("Switch case failed, power: " + power);
                break;
        }

        return(power);

    }

}
