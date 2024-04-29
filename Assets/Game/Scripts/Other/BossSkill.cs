using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : Bullet
{
    private void OnEnable()
    {
        OnDespawn(2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ConstantTag.ALLY))
        {
            if (attacker.faction == Faction.Enemy)
            {
                collision.GetComponent<Character>().OnHit(attacker.bulletDamage);
            }
        }
    }
}
