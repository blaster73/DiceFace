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

        TypeAdvantage advantage = FindTypeAdvantage(false);

        // Find out power
        int playerPower = player.powerDice.RollDie();
        int enemyPower = enemy.powerDice.RollDie();

        prototypeUIHandler.UpdatePowers(playerPower.ToString(), enemyPower.ToString());
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

    private TypeAdvantage FindTypeAdvantage(bool rolled)
    {
        // Find out types
        string playerType = player.typeDice.RollDie();
        string enemyType = enemy.typeDice.RollDie();
        prototypeUIHandler.PrintStuff("Player rolled " + playerType + " and enemy rolled " + enemyType);
        prototypeUIHandler.UpdateType(playerType, enemyType);

        bool playerReroll = false;
        bool enemyReroll = false;

        // Check if Player or Enemy has Type Reroll Ability Card
        if(enemy.abilityCard.ability == AbilityCard.Ability.TypeReroll){
            enemyReroll = true;
        }
        if(player.abilityCard.ability == AbilityCard.Ability.TypeReroll){
            playerReroll = true;
        }

        if(playerType == enemyType || playerType == "Eldritch" || enemyType == "Eldritch")
        {
            return(TypeAdvantage.Tie);
        }
        if(playerType == "Magic"){
            if(enemyType == "Radiation"){
                if(enemyReroll && !rolled){
                    prototypeUIHandler.PrintStuff("Enemy lost and rerolled type");
                    return(FindTypeAdvantage(true));
                }
                else{
                    return(TypeAdvantage.Player);
                }
            }
            if(enemyType == "Fire"){
                if(playerReroll && !rolled){
                    prototypeUIHandler.PrintStuff("Player lost and rerolled type");
                    return(FindTypeAdvantage(true));
                }
                else{
                    return(TypeAdvantage.Enemy);
                }
            }
        }
        if(playerType == "Radiation")
        {
            if(enemyType == "Fire")
                return(TypeAdvantage.Player);
            if(enemyType == "Magic")
                return(TypeAdvantage.Enemy);
        }
        if(playerType == "Fire")
        {
            if(enemyType == "Magic")
                return(TypeAdvantage.Player);
            if(enemyType == "Radiation")
                return(TypeAdvantage.Enemy);
        }
        return(TypeAdvantage.Tie);
    }

    private void CalculateDamage(int playerPower, int enemyPower)
    {
        if(player.abilityCard != null)
        {
            if(player.abilityCard.ability == AbilityCard.Ability.powDouble)
            {
                playerPower = playerPower * 2;
                prototypeUIHandler.PrintStuff("Player has double Power Ability Card, new power: " + playerPower);
            }
        }
        if(enemy.abilityCard != null)
        {
            if(enemy.abilityCard.ability == AbilityCard.Ability.powDouble)
            {
                enemyPower = enemyPower * 2;
                prototypeUIHandler.PrintStuff("Enemy has double Power Ability Card, new power: " + enemyPower);
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
