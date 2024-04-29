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
        GameManager.Instance.enemyTowerHp.text = hp.ToString();
    }

    public float GetHpNormalized()
    {
        return hp / maxHp;
    }

    protected virtual void Dead()
    {
        GameManager.Instance.Victory();
       
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
    public void Despawn()
    {
        Destroy(this.gameObject,1f);
    }

    public bool CanSpawnBoss()
    {
        if (hp <= maxHp * 0.2) // mau duoi 20 % => true
        {
            return true;
            
        }
        return false;
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
