using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float maxHp;

    private float hp;

    public float Hp { get => hp; set => hp = value; }

    private void Start()
    {
        hp = maxHp;
    }

    public float GetHpNormalized()
    {
        return hp / maxHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ConstantTag.ATTACKCOLLIDER))
        {
            AttackCollider attackCollider = collision.GetComponent<AttackCollider>();
            hp -= attackCollider.attacker.slashDamage;
        }
    }
}
