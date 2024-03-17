using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public Character attacker;
    public Character victim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        victim = collision.GetComponent<Character>();
        //if (attacker.target != null)
        //{
        //    victim = attacker.target;
        //}
        if (collision.CompareTag("Enemy") && attacker.faction == Faction.Ally && victim == attacker.target)
        {

            if (attacker.target != null)
            {
                victim = attacker.target;
                victim.OnHit(attacker.slashDamage);
            }
            if (victim != attacker.target)
            {
                victim.OnHit(0);
            }
            //if(collision.GetComponent<Character>().target != null)
            //{
            //    collision.GetComponent<Character>().target.OnHit(attacker.slashDamage);
            //    Debug.Log("Ally Attack");
            //}

        }
        if (collision.CompareTag("Ally") && attacker.faction == Faction.Enemy && victim == attacker.target)
        {
            //collision.GetComponent<Character>().OnHit(attacker.slashDamage);
            //Debug.Log("Enemy Attack");
            if (attacker.target != null)
            {
                victim = attacker.target;
                victim.OnHit(attacker.slashDamage);
            }
            if (victim != attacker.target)
            {
                victim.OnHit(0);
            }
        }

    }
}
