using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrototypeManager : MonoBehaviour
{

    enum TypeAdvantage {Player, Enemy, Tie};

    public Character player;
    public Character enemy;

    private PrototypeUIHandler prototypeUIHandler;

    void Start() {

        prototypeUIHandler = GetComponent<PrototypeUIHandler>();

        player = Instantiate(player);
        enemy = Instantiate(enemy);

        prototypeUIHandler.UpdateNames(player.name, enemy.name);
        prototypeUIHandler.UpdateHealths(player.health.ToString(), enemy.health.ToString());

    }

    [ContextMenu("Fight")]
    public void Fight()
    {
        // Find out types
        string playerType = player.typeDice.RollDie();
        string enemyType = enemy.typeDice.RollDie();
        prototypeUIHandler.PrintStuff("Player rolled " + playerType + " and enemy rolled " + enemyType);

        // Find out who type outcome
        TypeAdvantage advantage = TypeAdvantage.Tie;
        if(playerType == enemyType || playerType == "Eldritch" || enemyType == "Eldritch")
        {
            advantage = TypeAdvantage.Tie;
        }
        if(playerType == "Magic")
        {
            if(enemyType == "Radiation")
                advantage = TypeAdvantage.Player;
            if(enemyType == "Fire")
                advantage = TypeAdvantage.Enemy;
        }
        if(playerType == "Radiation")
        {
            if(enemyType == "Fire")
                advantage = TypeAdvantage.Player;
            if(enemyType == "Magic")
                advantage = TypeAdvantage.Enemy;
        }
        if(playerType == "Fire")
        {
            if(enemyType == "Magic")
                advantage = TypeAdvantage.Player;
            if(enemyType == "Radiation")
                advantage = TypeAdvantage.Enemy;
        }

        // Find out power
        int playerPower = player.powerDice.RollDie();
        int enemyPower = enemy.powerDice.RollDie();

        prototypeUIHandler.UpdateTypeAndPowers(playerType, playerPower.ToString(), enemyType, enemyPower.ToString());
        prototypeUIHandler.PrintStuff("Player rolled " + playerPower + " and enemy rolled " + enemyPower);



        switch (advantage)
        {
            case TypeAdvantage.Tie:
                 prototypeUIHandler.PrintStuff("Type tie, no power dice modification.");
                CalculateDamage(playerPower, enemyPower);
                break;
            case TypeAdvantage.Player:
                prototypeUIHandler.PrintStuff("Player won type, power doubled from " + playerPower + " to " + playerPower * 2);
                CalculateDamage(playerPower * 2, enemyPower);
                break;
            case TypeAdvantage.Enemy:
                prototypeUIHandler.PrintStuff("Enemy won type, power doubled from " + enemyPower + " to " + enemyPower * 2);
                CalculateDamage(playerPower, enemyPower * 2);
                break;
        }

        prototypeUIHandler.PrintStuff("---------------------------------------------------------------");

        /*if(player.health > 0 && enemy.health > 0)
        {
            Fight();
        }*/


    }

    private void CalculateDamage(int playerPower, int enemyPower)
    {
        if(player.abilityCard != null)
        {
            if(player.abilityCard.ability == AbilityCard.Ability.powDouble)
            {
                playerPower = playerPower * 2;
                prototypeUIHandler.PrintStuff("Player has double Power Ability Card");
            }
        }
        if(enemy.abilityCard != null)
        {
            if(enemy.abilityCard.ability == AbilityCard.Ability.powDouble)
            {
                enemyPower = enemyPower * 2;
                prototypeUIHandler.PrintStuff("Enemy has double Power Ability Card");
            }
        }

        int powerDif = Mathf.Abs(playerPower - enemyPower);

        if(playerPower == enemyPower)
        {
            Debug.Log("Tie!");
            prototypeUIHandler.PrintStuff("There was a tie, no damage dealt!");
        }
        else if(playerPower > enemyPower)
        {
            enemy.health -= 1;
            Debug.Log("Enemy took " + powerDif + " damage!");
            prototypeUIHandler.PrintStuff("Player won by: " + powerDif + "!");
            prototypeUIHandler.PrintStuff("Enemy takes 1 damage!");
        }
        else
        {
            player.health -= 1;
            Debug.Log("Player took " + powerDif + " damage!");
            prototypeUIHandler.PrintStuff("Enemy won by: " + powerDif + "!");
            prototypeUIHandler.PrintStuff("Player takes 1 damage!");
        }

        prototypeUIHandler.UpdateHealths(player.health.ToString(), enemy.health.ToString());

    }

}
