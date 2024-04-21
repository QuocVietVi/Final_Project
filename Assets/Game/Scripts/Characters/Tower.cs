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
        var level = LevelManager.Instance;
        data.golds += level.map.gold;
        data.gems += level.map.gem;
        //data.stars += level.stars;
        if (level.starsWin == 1 && level.stars == 3) 
        {
            data.stars += 1;
            level.stars -= 1;
        }
        if (level.starsWin == 2)
        {
            if (level.stars == 3)
            {
                data.stars += 2;
                level.stars -= 2;
            }
            if (level.stars == 2)
            {
                data.stars += 1;
                level.stars -= 1;
            }
        }
        if (level.starsWin == 3)
        {
            data.stars += level.stars;
            level.stars -= level.stars;
        }
        DataManager.Instance.SaveData(data);
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
