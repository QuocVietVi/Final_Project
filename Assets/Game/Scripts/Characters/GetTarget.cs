using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTarget : MonoBehaviour
{
    [SerializeField] private AllyAndEnemy allyAndEnemy;
    [SerializeField] private Player player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" )
        {
            if (allyAndEnemy != null && allyAndEnemy.faction == Faction.Ally && allyAndEnemy.AEType != AllyAndEnemyType.Healing)
            {
                allyAndEnemy.SetTarget(collision.GetComponent<Character>());

            }
            if (player != null)
            {
                player.SetTarget(collision.GetComponent<Character>());
            }
        }

        if (collision.tag == "Ally")
        {
            if (allyAndEnemy != null) 
            {
                if (allyAndEnemy.faction == Faction.Enemy)
                {

                    allyAndEnemy.SetTarget(collision.GetComponent<Character>());

                }
                if (allyAndEnemy.AEType == AllyAndEnemyType.Healing && allyAndEnemy.faction == Faction.Ally)
                {
                    allyAndEnemy.SetTarget(collision.GetComponent<Character>());

                }
            }

        }
    }
}
