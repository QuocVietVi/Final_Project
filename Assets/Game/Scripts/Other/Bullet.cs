using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    public AllyAndEnemy attacker;
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    private void OnEnable()
    {
        OnDespawn();
    }
    private void Move()
    {
        if (attacker.faction == Faction.Ally)
        {
            rb.velocity = Vector2.right * speed;

        }
        if (attacker.faction == Faction.Enemy)
        {
            rb.velocity = Vector2.left * speed;
        }

    }

    public void Despawn()
    {
        LeanPool.Despawn(this);
    }

    public void OnDespawn()
    {
        Invoke(nameof(Despawn), 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ConstantTag.ENEMY) && attacker.faction == Faction.Ally && attacker.AEType != AllyAndEnemyType.Healing)
        {
            collision.GetComponent<Character>().OnHit(attacker.bulletDamage);
            Despawn();
        }
        if (collision.CompareTag(ConstantTag.ALLY))
        {
            if (attacker.faction == Faction.Enemy)
            {
                collision.GetComponent<Character>().OnHit(attacker.bulletDamage);
                Despawn();
            }
            if (attacker.faction == Faction.Ally && attacker.AEType == AllyAndEnemyType.Healing)
            {
                collision.GetComponent<Character>().Healing(attacker.bulletDamage);
                Despawn();
            }

        }
        if (collision.CompareTag(ConstantTag.TOWER))
        {
            if (attacker.faction == Faction.Ally)
            {
                Tower tower = collision.GetComponent<Tower>();
                tower.OnHit(attacker.bulletDamage);
                Despawn();
            }
        }

    }
}
