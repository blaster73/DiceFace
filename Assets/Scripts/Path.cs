using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{

    [SerializeField]
    private Character[] enemies;
    [SerializeField]
    private AbilityCard[] cards;

    private PathUIHandler pathUIHandler;

    void Start()
    {
        pathUIHandler = GetComponent<PathUIHandler>();
    }

    [ContextMenu("Test button update")]
    public void RandomizeNextEnemy()
    {
        int rEnemyIndex = Random.Range(0, enemies.Length);
        int rCardIndex = Random.Range(0, cards.Length);

        pathUIHandler.UpdateEnemyButtons(enemies[Random.Range(0, enemies.Length)], enemies[Random.Range(0, enemies.Length)]);
    }
}
