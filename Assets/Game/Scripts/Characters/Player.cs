using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject attackCollider1, attackCollider2;
    [SerializeField] private List<AllyAndEnemy> allies = new List<AllyAndEnemy>();
    [SerializeField] private float maxEnergy, maxMana;
    [SerializeField] private List<SkillCollider> listSkills = new List<SkillCollider>();
    [SerializeField] private float manaRecovery, energyRecovery;
    [SerializeField] private float attackEnergyNeeded;
    [SerializeField] private float skillRange;
    [Space(5)]
    [Header("Setup")]
    [SerializeField] private List<DropSlot> setUpItems;
    [SerializeField] private GameObject weapon;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private SpriteRenderer shield;

    private float horizontal;
    private float vertical;
    //private bool onFirstFloor, onSecondFloor;
    private bool isAttack, isRun;
    private bool canCallAlly1, canCallAlly2, canCallAlly3;
    private bool doSkill;
    private float energy;
    private float mana;
    private List<AllyAndEnemy> maxAllies = new List<AllyAndEnemy>();
    private Vector3 spawnPos;
    private List<Character> listSkillTargets = new List<Character>();
    private Character skillTarget;
    

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

    private void Awake()
    {
        setUpItems = SetupManager.Instance.dropSlots;
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
        RemoveAllies();
        target = FindTarget();
        skillTarget = FindTarget2();
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

        //Game Ui
        var game = GameManager.Instance;
        game.SetText(game.playerHp, Mathf.FloorToInt(Hp), maxHp);
        game.SetText(game.mana, Mathf.FloorToInt(mana), maxMana);
        game.SetText(game.energy, Mathf.FloorToInt(energy), maxEnergy);

    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeAnim(ConstantAnim.IDLE);
        var data = SODataManager.Instance;
        maxHp = data.PlayerData.maxHp;
        maxMana = data.PlayerData.maxMana;
        maxEnergy = data.PlayerData.maxEnergy;
        Hp = maxHp;
        Mana = 0;
        Energy = 0;

        //Setup
        //Ally
        var allyInfo = GameManager.Instance;
        allies[0] = GetAllyData(setUpItems[0].allyType).allyPrefab;
        allies[1] = GetAllyData(setUpItems[1].allyType).allyPrefab;
        allies[2] = GetAllyData(setUpItems[2].allyType).allyPrefab;
        //image and text
        for (int i = 0; i < allyInfo.allySlots.Count; i++)
        {
            if (allies[i] != null)
            {
                allyInfo.allySlots[i].sprite = GetAllyData(setUpItems[i].allyType).image;
                allyInfo.alliesEnergy[i].text = allies[i].energyNedded.ToString();
            }
            else
            {
                allyInfo.allySlots[i].sprite = GameManager.Instance.isLokingPic;
                allyInfo.alliesEnergy[i].text = "0";
            }
        }
        //allyImages[0].sprite = GetAllyData(setUpItems[0].allyType).image;
        //allyImages[1].sprite = GetAllyData(setUpItems[1].allyType).image;
        //allyImages[2].sprite = GetAllyData(setUpItems[2].allyType).image;
        //Skill
        var skillImages = GameManager.Instance.skillSlots;
        listSkills[0] = GetSkillData(setUpItems[3].skillType).skillPrefab;
        listSkills[1] = GetSkillData(setUpItems[4].skillType).skillPrefab;
        if (listSkills[0] != null)
        {
            skillImages[0].sprite = GetSkillData(setUpItems[3].skillType).image;
        }
        else
        {
            skillImages[0].sprite = GameManager.Instance.isLokingPic;
        }
        if (listSkills[1] != null)
        {
            skillImages[1].sprite = GetSkillData(setUpItems[4].skillType).image;
        }
        else
        {
            skillImages[1].sprite = GameManager.Instance.isLokingPic;
        }

        //Weapon
        var wData = SODataManager.Instance.GetWeaponData(setUpItems[5].weaponType);
        weapon = Instantiate(wData.weaponPrefab, weaponHolder);
        slashDamage += wData.damage;
        //Shield
        var sData = SODataManager.Instance.GetShieldData(setUpItems[6].shieldType);
        shield.sprite = sData.image;
    }
    private AllyData GetAllyData(AllyType type)
    {
        return SODataManager.Instance.GetAllyData(type);
    }
    private SkillData GetSkillData(SkillType type)
    {
        return SODataManager.Instance.GetSkillData(type);
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
        if (energy >= allies.energyNedded && maxAllies.Count < LevelManager.Instance.map.maxAllies)
        {
            AllyAndEnemy ally = Instantiate(allies, spawnPos, Quaternion.Euler(Vector3.zero));
            
            energy -= allies.energyNedded;
            maxAllies.Add(ally);
        }


    }

    private IEnumerator SpawnAlly1()
    {
        if (allies[0] != null)
        {
            yield return new WaitForSeconds(allies[0].delayTime);
            SpawnAlly(allies[0]);
            canCallAlly1 = true;
        }

    }

    private IEnumerator SpawnAlly2()
    {
        if (allies[1] != null)
        {
            yield return new WaitForSeconds(allies[1].delayTime);
            SpawnAlly(allies[1]);
            canCallAlly2 = true;
        }

    }

    private IEnumerator SpawnAlly3()
    {
        if (allies[2] != null)
        {
            yield return new WaitForSeconds(allies[2].delayTime);
            SpawnAlly(allies[2]);
            canCallAlly3 = true;
        }
    }
    //----------------- End Spawn ally ------------------

    //----------------- Attack ------------------
    private void Attack(string animName)
    {
        anim.SetTrigger(animName);
        rb.velocity = Vector2.zero;
        isRun = false;
        energy -= attackEnergyNeeded;
        SetAttack();
        Invoke(nameof(CanRun), 0.7f);

    }

    private void Shash()
    {
        if (energy >= attackEnergyNeeded)
        {
            Attack(ConstantAnim.ATTACK);
            attackCollider1.SetActive(true);
            Invoke(nameof(ResetAttack), 0.5f);
        }
            
    }

    private void Poked()
    {
        if (energy >= attackEnergyNeeded)
        {
            Attack(ConstantAnim.ATTACK2);
            attackCollider2.SetActive(true);
            Invoke(nameof(ResetAttack), 0.5f);
        }

    }

    //------------------ End Attack ---------------------

    // ----------------- Skill ----------------------

    private void UseSkill(string skill, SkillCollider skillPrefab)
    {   
        if (skillPrefab != null)
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
                if (target == null && skillTarget != null)
                {
                    LeanPool.Spawn(skillPrefab, skillTarget.transform.position, Quaternion.Euler(Vector2.zero));
                }
                Invoke(nameof(ResetSkill), skillPrefab.despawnTime * 2);
            }
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
        Portal portal = LevelManager.Instance.map.portal;
        var hpPercentage = Hp / maxHp * 100;
        var portalHpPercentage = portal.Hp / portal.maxHp * 100;

        if (Hp >= maxHp * 80 / 100 && hpPercentage >= 80 && portal.Hp >= portal.maxHp * 80/100)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
        else if ((Hp < maxHp * 80 / 100 && Hp >= maxHp * 30/100) 
            || (portal.Hp < portal.maxHp * 80 / 100 && portal.Hp >= portal.maxHp * 30 / 100))
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(false);
        }
        //if (Hp < maxHp * 30 / 100)
        else
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

    private void RemoveAllies()
    {
        if (maxAllies.Count > 0)
        {
            for (int i = 0; i < maxAllies.Count; i++)
            {
                if (maxAllies[i] == null)
                {
                    maxAllies.RemoveAt(i);
                    Debug.Log("remove");
                }
            }
        }

    }

    public void SetTextAlly(TextMeshProUGUI ally)
    {
        ally.text = maxAllies.Count.ToString();
    }

    public virtual void SetTarget2(Character character)
    {
        listSkillTargets.Add(character);
    }


    public Character FindTarget2()
    {
        Character target = null;
        for (int i = 0; i < listSkillTargets.Count; i++)
        {
            if (listSkillTargets[i] != null && Vector2.Distance(listSkillTargets[i].transform.position, transform.position) < skillRange)
            {
                target = listSkillTargets[i];
            }
        }
        return target;
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
