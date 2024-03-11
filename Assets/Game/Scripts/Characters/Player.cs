using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private LayerMask firstFloor, secondFloor;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject attackCollider1, attackCollider2;
    [SerializeField] private List<AllyAndEnemy> allies = new List<AllyAndEnemy>();
    [SerializeField] private float maxEnergy, maxMana;
    [SerializeField] private Vector2 offset;
    [SerializeField] private GameObject skillPrefab;


    private float horizontal;
    private float vertical;
    //private bool onFirstFloor, onSecondFloor;
    private bool isAttack, isRun;
    private bool canCallAlly1, canCallAlly2, canCallAlly3;
    private bool doSkill1, doSkill2, doSkill3;
    private float energy;
    private float mana;
    private Vector3 spawnPos;
   

    public float Energy
    {
        get { return energy; }
        set
        {
            energy = value;
            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }
        }
    }
    public float Mana
    {
        get { return mana; }
        set
        {
            mana = value;
            if (mana > maxMana)
            {
                mana = maxMana;
            }
        }
    }

    private void Start()
    {
        isRun = true;
        canCallAlly1 = true;
        canCallAlly2 = true;
        canCallAlly3 = true;
        OnInit();
        
    }
    private void FixedUpdate()
    {

        target = FindTarget();
        Debug.DrawLine(transform.position, transform.position + Vector3.right *attackRange, Color.red);
        Move();
        spawnPos = new Vector3(spawnPoint.position.x, Random.Range(spawnPoint.position.y, spawnPoint.position.y + 0.7f), 0);
        if (isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (Input.GetKey(KeyCode.J) && isAttack == false)
        {
            Shash();

        }

        if (Input.GetKey(KeyCode.K) && isAttack == false)
        {
            Poked();

        }
        if (Input.GetKey(KeyCode.U) && doSkill1 == false)
        {
            Skill("Skill1");

        }
        if (Input.GetKey(KeyCode.I) && isAttack == false)
        {
            Skill("Skill2");

        }

        if (Input.GetKey(KeyCode.Space) && isAttack == false)
        {
            Attack("Shoot");
        }

        if (Input.GetKey(KeyCode.Alpha1) && canCallAlly1 == true)
        {
            //Invoke(nameof(SpawnAlly1), allies[0].delayTime);
            StartCoroutine(SpawnAlly1());
            canCallAlly1 = false;
        }

        if (Input.GetKey(KeyCode.Alpha2) && canCallAlly2 == true)
        {
            //Invoke(nameof(SpawnAlly2), allies[1].delayTime);
            StartCoroutine(SpawnAlly2());
            canCallAlly2 = false;
        }

        if (Input.GetKey(KeyCode.Alpha3) && canCallAlly3 == true)
        {
            //Invoke(nameof(SpawnAlly3), allies[2].delayTime);
            StartCoroutine(SpawnAlly3());
            canCallAlly3 = false;
        }



    }


    private void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // lấy điều khiển từ bàn phím (chiều ngang)
        if (Mathf.Abs(horizontal) > 0.1f && isRun == true) // khi bấm phím
        {
            ChangeAnim("Run");
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            //horizontal > 0 -> trả về -0.6, nếu horizontal <= 0 -> trả về 0.6
            transform.localScale = new Vector3(horizontal > 0 ? -0.6f : 0.6f, 0.6f, 0.6f);
        }
        else 
        {
            ChangeAnim("Idle");
            rb.velocity = Vector2.zero;
        }

    }

    //private bool IsGrounded(LayerMask floor)
    //{
    //    //chiếu 1 tia xuống để kiểm tra
    //    Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.8f, Color.red);
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, floor);

    //    //if (hit.collider != null)
    //    //{
    //    //    return true;
    //    //}
    //    //else
    //    //{
    //    //    return false;
    //    //}
    //    return hit.collider != null;
    //}

    private IEnumerator SpawnAlly1()
    {
        //Instantiate(allies[0], spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(allies[0].delayTime);
        //Instantiate(allies[0], spawnPos, spawnPoint.rotation);
        AllyAndEnemy ally = Instantiate(allies[0], spawnPos, Quaternion.Euler(Vector3.zero));
        canCallAlly1 = true;

    }

    private IEnumerator SpawnAlly2()
    {
        yield return new WaitForSeconds(allies[1].delayTime);
        //Instantiate(allies[1], spawnPos, spawnPoint.rotation);
        AllyAndEnemy ally = Instantiate(allies[1], spawnPos, Quaternion.Euler(Vector3.zero));
        canCallAlly2 = true;

    }

    private IEnumerator SpawnAlly3()
    {
        yield return new WaitForSeconds(allies[2].delayTime);
        //Instantiate(allies[2], spawnPos, spawnPoint.rotation);
        AllyAndEnemy ally = Instantiate(allies[2], spawnPos, Quaternion.Euler(Vector3.zero));
        canCallAlly3 = true;

    }

    private void Attack(string animName)
    {
        anim.SetTrigger(animName);
        rb.velocity = Vector2.zero;
        isRun = false;
        SetAttack();
        Invoke(nameof(ResetAttack), 0.5f);
        Invoke(nameof(CanRun), 0.7f);
    }

    private void Shash()
    {
        Attack("Attack");
        attackCollider1.SetActive(true);
    }

    private void Poked()
    {
        Attack("Attack2");
        attackCollider2.SetActive(true);
    }

    private void Skill(string skill)
    {
        anim.SetTrigger(skill);
        rb.velocity = Vector2.zero;
        isRun = false;
        Invoke(nameof(CanRun), 0.7f);
        if(target != null)
        {
            LeanPool.Spawn(skillPrefab, target.transform.position, Quaternion.Euler(Vector2.zero));
        }
        doSkill1 = true;
        Invoke(nameof(ResetSkill), 1f);
    }

    private void ResetAttack()
    {
        isAttack = false;
        attackCollider1.SetActive(false);
        attackCollider2.SetActive(false);
    }

    private void SetAttack()
    {
        isAttack = true;
    }

    private void CanRun()
    {
        isRun = true;
    }

    private void ResetSkill()
    {
        doSkill1 = false;
    }

    //private AllyAndEnemy FindEnemy(LayerMask enemy)
    //{
    //    Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.8f, Color.red);
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, enemy);
    //    AllyAndEnemy target = hit.collider.GetComponent<AllyAndEnemy>();
    //    if (target.faction == Faction.Enemy)
    //    {
    //        enemies.Add(target);
    //    }

    //    return target;
    //}

}
