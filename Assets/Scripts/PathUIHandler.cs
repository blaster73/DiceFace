using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PathUIHandler : MonoBehaviour
{

    [SerializeField]
    private GameObject pathModal;
    [SerializeField]
    private TMP_Text[] buttons;

    public void UpdateEnemyButtons(Character enemy1, Character enemy2)
    {
        pathModal.SetActive(true);
        buttons[0].text = enemy1.characterName;
        buttons[1].text = "Health: " + enemy1.health.ToString() + 
            "\nPower: " + enemy1.powerDice.sides +
            "\nTypes: " + Types(enemy1.typeDice) + 
            "\nAbility Card: " + Enum.GetName(typeof(AbilityCard.Ability), enemy1.abilityCard.ability);
            
        buttons[2].text = enemy2.characterName;
        buttons[3].text = "Health: " + enemy2.health.ToString() + 
            "\nPower: " + enemy2.powerDice.sides +
            "\nTypes: " + Types(enemy2.typeDice) + 
            "\nAbility Card: " + Enum.GetName(typeof(AbilityCard.Ability), enemy2.abilityCard.ability);
    }

    private string Types(TypeDice typeDice)
    {
        string result = "";
        for(int i = 0; i < typeDice.sideTypes.Length; i++)
        {
            result += Enum.GetName(typeof(TypeDice.Type), typeDice.sideTypes[i]) + ", ";
        }
        return result;
    }

    public void SelectEnemy()
    {
        
    }

}
