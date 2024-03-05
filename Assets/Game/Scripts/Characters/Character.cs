using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Character : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;
    [SerializeField] protected float speed;
    [SerializeField] protected float maxHp;
    [SerializeField] protected Transform throwPoint;
    [SerializeField] protected float attackRange;
    private float hp;
    private string currentAnimName;

    protected List<Character> targets = new List<Character>();
    public Character target;
    public float bulletDamage;
    public float slashDamage;
    public Faction faction;
    public bool IsDead => hp <= 0;

    public float Hp
    {
        get { return hp; }
        set 
        {
            hp = value; 
            if (hp > maxHp)
            {
                hp = maxHp;
            }
        }
    }

    protected virtual void OnInit()
    {
        hp = maxHp;
    }
    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    protected virtual void Dead()
    {
        ChangeAnim("Die");
        speed = 0;
    }

    public void OnHit(float damage)
    {
        if (!IsDead)
        {
            hp -= damage;
            if (IsDead)
            {
                hp = 0;
                Dead();
            }
            //healthBar.SetNewHp(hp);
            //Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }
    }

    public void Healing(float heal)
    {
        if (!IsDead && hp < maxHp)
        {
            hp += heal;
        } 
    }

    public virtual void SetTarget(Character character)
    {
        targets.Add(character);
    }


    public Character FindTarget()
    {
        Character target = null;
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null && Vector2.Distance(targets[i].transform.position, transform.position) < attackRange)
            {
                target = targets[i];
            }
        }
        return target;
    }


}
