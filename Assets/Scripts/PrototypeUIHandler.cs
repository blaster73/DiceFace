using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrototypeUIHandler : MonoBehaviour
{

    public TMP_Text[] TMP_player;
    public TMP_Text[] TMP_enemy;
    public TMP_Text TMP_results;

    private PrototypeManager prototypeManager;

    void Start()
    {
        prototypeManager = GetComponent<PrototypeManager>();
    }

    public void PrintStuff(string s)
    {
        TMP_results.text += "\n" + s;
    }

    public void UpdateNames(string playerName, string enemyName)
    {
        TMP_player[0].text = playerName;
        TMP_enemy[0].text = enemyName;
    }

    public void UpdateHealths(string playerHealth, string enemyHealth)
    {
        TMP_player[1].text = playerHealth;
        TMP_enemy[1].text = enemyHealth;
    }

    public void UpdateTypeAndPowers(string playerType, string playerPower, string enemyType, string enemyPower)
    {
        TMP_player[2].text = playerType;
        TMP_player[3].text = playerPower;

        TMP_enemy[2].text = enemyType;
        TMP_enemy[3].text = enemyPower;
    }
}
