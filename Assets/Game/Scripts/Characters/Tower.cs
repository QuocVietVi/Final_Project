using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float maxHp;

    private float hp;

    public float Hp { get => hp; set => hp = value; }

    public bool IsDead => hp <= 0;

    private void Start()
    {
        hp = maxHp;
    }

    public float GetHpNormalized()
    {
        return hp / maxHp;
    }

    protected virtual void Dead()
    {
        GameManager.Instance.Victory();
        var data = SODataManager.Instance.PlayerData;
        var map = LevelManager.Instance.map;
        data.golds += map.gold;
        data.gems += map.gem;
        
    }

    public void OnHit(float damage)
    {
        if (!IsDead)
        {
            hp -= damage;
            GameManager.Instance.enemyTowerHp.text = hp.ToString();
            if (IsDead)
            {
                hp = 0;
                Invoke(nameof(Dead),0.5f);
            }
            //healthBar.SetNewHp(hp);
            //Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ConstantTag.ATTACKCOLLIDER))
        {
            AttackCollider attackCollider = collision.GetComponent<AttackCollider>();
            //hp -= attackCollider.attacker.slashDamage;
            OnHit(attackCollider.attacker.slashDamage);
        }
    }
}
