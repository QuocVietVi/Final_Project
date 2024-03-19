using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AllyAndEnemyType
{
    CloseRange = 0,
    LongRange = 1,
    Healing = 2,
    Boss = 3
}
public enum Faction
{
    Enemy = 0,
    Ally = 1
}
public class AllyAndEnemy : Character
{
    
    [SerializeField] private GameObject getTarget;
    [SerializeField] private AttackCollider attackCollider;
    [SerializeField] private Bullet bullet;
    private IState currentState;
    private bool canAttack;

    
    public float delayTime;
    public float energyNedded;
    public AllyAndEnemyType AEType;
    


    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExcute(this);
        }
        if(target == null)
        {
            target = FindTarget();
        }

        if (target != null && target.IsDead)
        {
            ChangeAnim("Idle");
        }
        //if (target != null)
        //{
        //    getTarget.SetActive(false);
        //}
        //if (target == null)
        //{
        //    getTarget.SetActive(true);
        //}
        if (target != null && Vector2.Distance(target.transform.position, transform.position) > attackRange)
        {
            target = null;
        }
        //if (GameManager.Instance.map.tower != null)
        //{
        //    if (Vector2.Distance(GameManager.Instance.map.tower.transform.position, this.transform.position) < attackRange *3)
        //    {
        //        ChangeState(new AttackState());
        //    }
        //}

    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new MoveState());
        speed = 3;

    }

    private void Despawn()
    {
        Destroy(this.gameObject);
    }
    protected override void Dead()
    {
        base.Dead();
        Invoke(nameof(Despawn), 1.5f);
        this.GetComponent<Collider2D>().enabled = false;
        ChangeState(null);
        target = null;
        rb.velocity = Vector2.zero;
        targets.Clear();
    }

    public void Move()
    {
        ChangeAnim("Run");
        //rb.velocity = transform.right * speed;
        if (this.faction == Faction.Enemy)
        {
            rb.velocity = Vector2.left * speed;
        }
        if (this.faction == Faction.Ally)
        {
            rb.velocity = Vector2.right * speed;
        }

    }


    public void StopMoving()
    {
        ChangeAnim("Idle");
        rb.velocity = Vector2.zero;
    }

    public void Attack()
    {
        ChangeAnim("Attack");
        if (this.AEType == AllyAndEnemyType.CloseRange)
        {
            SetAttack();
            Invoke(nameof(ResetAttack), 0.5f);
        }
        if (this.AEType == AllyAndEnemyType.Healing)
        {
            Invoke(nameof(Shoot), 1f);
        }
        if (this.AEType == AllyAndEnemyType.LongRange)
        {
            Invoke(nameof(Shoot), 0.75f);
        }

    }
    private void Shoot()
    {
        Bullet b = LeanPool.Spawn(bullet, throwPoint.position, throwPoint.rotation);
        b.attacker = this;
    }

    private void Slash()
    {

    }
    public bool TargetInAttackRange()
    {
        if (target !=null && Vector2.Distance(target.transform.position , transform.position) < attackRange)
        {
            return true;
        }
        if (LevelManager.Instance.map.tower != null && 
            Vector2.Distance(LevelManager.Instance.map.tower.transform.position, this.transform.position) < attackRange +3.5f &&
            this.faction == Faction.Ally)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void SetTarget(Character character)
    {
        base.SetTarget(character);

        if (TargetInAttackRange())
        {
            ChangeState(new AttackState());
        }

    }
 
    
    //public override Character FindTarget()
    //{
    //    Character target = null;
    //    for (int i = 0; i < targets.Count; i++)
    //    {
    //        if (targets[i] != null && Vector2.Distance(targets[i].transform.position, transform.position) < attackRange)
    //        {
    //            target = targets[i];
    //        }
    //    }
    //    return target;
    //}

    public void ChangeState(IState newState)
    {
        //khi đổi sang state mới, check xem state cũ có = null ko
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    private void ResetAttack()
    {
        attackCollider.gameObject.SetActive(true);
    }

    private void SetAttack()
    {
        attackCollider.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Attack Collider")
        {
            //ChangeAnim("Hurt");
        }
    }

}
