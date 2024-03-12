using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollider : MonoBehaviour
{
    [SerializeField] private float skillDamage;
    [SerializeField] private Collider2D col;
    public float despawnTime;
    public int manaNeeded;

    private void Start()
    {
        InvokeRepeating(nameof(Despawn), despawnTime, despawnTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            col.enabled = true;
            collision.GetComponent<AllyAndEnemy>().OnHit(skillDamage);
            Invoke(nameof(ResetAttack), 0.5f);
            //Invoke(nameof(Despawn), 1f);
        }
    }

    private void Despawn()
    {
        LeanPool.Despawn(this);

    }
    private void ResetAttack()
    {
        col.enabled = false;
    }
}
