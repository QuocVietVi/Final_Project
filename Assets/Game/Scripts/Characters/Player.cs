using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject attackCollider1, attackCollider2;
    [SerializeField] private List<AllyAndEnemy> allies = new List<AllyAndEnemy>();
    [SerializeField] private float maxEnergy, maxMana;
    [SerializeField] private List<SkillCollider> listSkills = new List<SkillCollider>();
    [SerializeField] private float manaRecovery, energyRecovery;

    private float horizontal;
    private float vertical;
    //private bool onFirstFloor, onSecondFloor;
    private bool isAttack, isRun;
    private bool canCallAlly1, canCallAlly2, canCallAlly3;
    private bool doSkill;
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
        spawnPoint = LevelManager.Instance.map.allySpawnPoint;
    }
    private void FixedUpdate()
    {

        target = FindTarget();
        Debug.DrawLine(transform.position, transform.position + Vector3.right *attackRange, Color.red);
        if (!IsDead)
        {
            Move();
        }
        spawnPos = new Vector3(spawnPoint.position.x, Random.Range(spawnPoint.position.y, spawnPoint.position.y + 0.7f), 0);
        if (isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        Mana += manaRecovery * Time.fixedDeltaTime;
        Energy += energyRecovery * Time.fixedDeltaTime;

        // --------------------- Input form keyboard -------------------------
        if (Input.GetKey(KeyCode.J) && isAttack == false)
        {
            Shash();

        }

        if (Input.GetKey(KeyCode.K) && isAttack == false)
        {
            Poked();

        }
        if (Input.GetKey(KeyCode.U) && doSkill == false)
        {
            Skill1();

        }
        if (Input.GetKey(KeyCode.I) && doSkill == false)
        {
            Skill2();

        }

        if (Input.GetKey(KeyCode.Space) && isAttack == false)
        {
            Attack("Shoot");
        }

        if (Input.GetKey(KeyCode.Q) && canCallAlly1 == true)
        {
            //Invoke(nameof(SpawnAlly1), allies[0].delayTime);
            StartCoroutine(SpawnAlly1());
            canCallAlly1 = false;
        }

        if (Input.GetKey(KeyCode.W) && canCallAlly2 == true)
        {
            //Invoke(nameof(SpawnAlly2), allies[1].delayTime);
            StartCoroutine(SpawnAlly2());
            canCallAlly2 = false;
        }

        if (Input.GetKey(KeyCode.E) && canCallAlly3 == true)
        {
            //Invoke(nameof(SpawnAlly3), allies[2].delayTime);
            StartCoroutine(SpawnAlly3());
            canCallAlly3 = false;
        }
        // ----------------------- End input from keyboard -----------------------

        

    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeAnim(ConstantAnim.IDLE);
        Mana = maxMana;
        Energy = maxEnergy;
    }

    protected override void Dead()
    {
        base.Dead();
        GameManager.Instance.GameOver();
    }

    //--------------- Move -------------------
    private void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // lấy điều khiển từ bàn phím (chiều ngang)
        if (Mathf.Abs(horizontal) > 0.1f && isRun == true) // khi bấm phím
        {
            ChangeAnim(ConstantAnim.RUN);
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            //horizontal > 0 -> trả về -0.6, nếu horizontal <= 0 -> trả về 0.6
            transform.localScale = new Vector3(horizontal > 0 ? -0.6f : 0.6f, 0.6f, 0.6f);
        }
        else 
        {
            ChangeAnim(ConstantAnim.IDLE);
            rb.velocity = Vector2.zero;
            transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f);
        }

    }

    //--------------------- End mov ----------------------


    //--------------------Spawn allies---------------------

    private void SpawnAlly(AllyAndEnemy allies)
    {
        if (energy >= allies.energyNedded)
        {
            AllyAndEnemy ally = Instantiate(allies, spawnPos, Quaternion.Euler(Vector3.zero));
            
            energy -= allies.energyNedded;
        }


    }

    private IEnumerator SpawnAlly1()
    {
        yield return new WaitForSeconds(allies[0].delayTime);
        SpawnAlly(allies[0]);
        canCallAlly1 = true;
    }

    private IEnumerator SpawnAlly2()
    {
        yield return new WaitForSeconds(allies[1].delayTime);
        SpawnAlly(allies[1]);
        canCallAlly2 = true;
    }

    private IEnumerator SpawnAlly3()
    {
        yield return new WaitForSeconds(allies[2].delayTime);
        SpawnAlly(allies[2]);
        canCallAlly3 = true;
    }
    //----------------- End Spawn ally ------------------

    //----------------- Attack ------------------
    private void Attack(string animName)
    {
        if (energy >= 20)
        {
            anim.SetTrigger(animName);
            rb.velocity = Vector2.zero;
            isRun = false;
            energy -= 20;
            SetAttack();

            Invoke(nameof(CanRun), 0.7f);
        }

    }

    private void Shash()
    {
        Attack(ConstantAnim.ATTACK);
        attackCollider1.SetActive(true);
        Invoke(nameof(ResetAttack), 0.5f);
    }

    private void Poked()
    {
        Attack(ConstantAnim.ATTACK2);
        attackCollider2.SetActive(true);
        Invoke(nameof(ResetAttack), 0.5f);
    }

    //------------------ End Attack ---------------------

    // ----------------- Skill ----------------------

    private void UseSkill(string skill, SkillCollider skillPrefab)
    {   
        if (mana >= skillPrefab.manaNeeded)
        {
            anim.SetTrigger(skill);
            rb.velocity = Vector2.zero;
            mana -= skillPrefab.manaNeeded;
            isRun = false;
            doSkill = true;
            Invoke(nameof(CanRun), 0.7f);
            if (target != null)
            {
                LeanPool.Spawn(skillPrefab, target.transform.position, Quaternion.Euler(Vector2.zero));
            }
            Invoke(nameof(ResetSkill), skillPrefab.despawnTime * 2);
        }
        
    }

    private void Skill1()
    {
        UseSkill(ConstantAnim.SKILL1, listSkills[0]);
    }

    private void Skill2()
    {
        UseSkill(ConstantAnim.SKILL2, listSkills[1]);
    }

    // ------------------ End skill --------------------

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
        doSkill = false;
    }



    public float GetManaNormalized()
    {
        return mana/maxMana;
    }

    public float GetEnergyNormalized()
    {
        return energy / maxEnergy;
    }

    public void StarLevel(GameObject star1, GameObject star2, GameObject star3)
    {
        if (Hp >= maxHp * 80 / 100)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
        if (Hp < maxHp * 80 / 100 && Hp >= maxHp * 30/100)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(false);
        }
        if (Hp < maxHp * 30 / 100)
        {
            star1.SetActive(true);
            star2.SetActive(false);
            star3.SetActive(false);
        }
    }

    public void Revive()
    {
        Hp = maxHp * 80 / 100; 
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
