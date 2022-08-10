using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrototypeManager : MonoBehaviour
{

    enum TypeAdvantage {Player, Enemy, Tie};

    public Character player;
    public Character enemy;

    public TMP_Text[] TMP_player;
    public TMP_Text[] TMP_enemy;
    public TMP_Text TMP_results;

    void Start() {

        player = Instantiate(player);
        enemy = Instantiate(enemy);

        TMP_player[0].text = player.name;
        TMP_player[1].text = player.health.ToString();
        TMP_enemy[0].text = enemy.name;
        TMP_enemy[1].text = enemy.health.ToString();
    }

    [ContextMenu("Fight")]
    public void Fight()
    {
        // Find out types
        string playerType = player.typeDice.RollDie();
        string enemyType = enemy.typeDice.RollDie();
        TMP_results.text += "\nPlayer rolled " + playerType + " and enemy rolled " + enemyType;

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
        TMP_player[2].text = playerType;
        TMP_player[3].text = playerPower.ToString();

        int enemyPower = enemy.powerDice.RollDie();
        TMP_enemy[2].text = enemyType;
        TMP_enemy[3].text = enemyPower.ToString();
        TMP_results.text += "\nPlayer rolled " + playerPower + " and enemy rolled " + enemyPower;



        switch (advantage)
        {
            case TypeAdvantage.Tie:
                TMP_results.text += "\nType tie, no power dice modification.";
                CalculateDamage(playerPower, enemyPower);
                break;
            case TypeAdvantage.Player:
                TMP_results.text += "\nPlayer won type, power doubled from " + playerPower + " to " + playerPower * 2;
                CalculateDamage(playerPower * 2, enemyPower);
                break;
            case TypeAdvantage.Enemy:
                TMP_results.text += "\nEnemy won type, power doubled from " + enemyPower + " to " + enemyPower * 2;
                CalculateDamage(playerPower, enemyPower * 2);
                break;
        }

        TMP_results.text += "\n---------------------------------------------------------------";

        /*if(player.health > 0 && enemy.health > 0)
        {
            Fight();
        }*/


    }

    private void CalculateDamage(int playerPower, int enemyPower)
    {
        int powerDif = Mathf.Abs(playerPower - enemyPower);

        if(playerPower == enemyPower)
        {
            Debug.Log("Tie!");
            TMP_results.text += "\nThere was a tie, no damage dealt!";
        }
        else if(playerPower > enemyPower)
        {
            enemy.health -= 1;
            Debug.Log("Enemy took " + powerDif + " damage!");
            TMP_results.text += "\nPlayer won by: " + powerDif + "!";
            TMP_results.text += "\nEnemy takes 1 damage!";
        }
        else
        {
            player.health -= 1;
            Debug.Log("Player took " + powerDif + " damage!");
            TMP_results.text += "\nEnemy won by: " + powerDif + "!";
            TMP_results.text += "\nPlayer takes 1 damage!";
        }

        TMP_player[1].text = player.health.ToString();
        TMP_enemy[1].text = enemy.health.ToString();

    }

}
