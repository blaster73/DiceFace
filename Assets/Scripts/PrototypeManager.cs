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
        // Find out type outcome
        string playerType = player.typeDice.RollDie();
        string enemyType = enemy.typeDice.RollDie();

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
        Debug.Log("Player rolled Type " + playerType + " at power: " + playerPower);
        TMP_player[2].text = playerType;
        TMP_player[3].text = playerPower.ToString();

        int enemyPower = enemy.powerDice.RollDie();
        Debug.Log("Enemy rolled Type " + enemyType + " at power: " + enemyPower);
        TMP_enemy[2].text = enemyType;
        TMP_enemy[3].text = enemyPower.ToString();

        switch (advantage)
        {
            case TypeAdvantage.Tie:
                CalculateDamage(playerPower, enemyPower);
                break;
            case TypeAdvantage.Player:
                CalculateDamage(playerPower * 2, enemyPower);
                break;
            case TypeAdvantage.Enemy:
                CalculateDamage(playerPower, enemyPower * 2);
                break;
        }


    }

    private void CalculateDamage(int playerPower, int enemyPower)
    {
        int powerDif = Mathf.Abs(playerPower - enemyPower);

        if(playerPower == enemyPower)
        {
            Debug.Log("Tie!");
            TMP_results.text += "\nTie!";
        }
        if(playerPower > enemyPower)
        {
            enemy.health -= powerDif;
            Debug.Log("Enemy took " + powerDif + " damage!");
            TMP_results.text += "\nEnemy took " + powerDif + " damage!";
        }
        else
        {
            player.health -= powerDif;
            Debug.Log("Player took " + powerDif + " damage!");
            TMP_results.text += "\nPlayer took " + powerDif + " damage!";
        }

        TMP_player[1].text = player.health.ToString();
        TMP_enemy[1].text = enemy.health.ToString();

    }

}
