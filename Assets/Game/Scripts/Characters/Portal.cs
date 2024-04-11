using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public float maxHp;

    private float hp;

    public float Hp { get => hp; set => hp = value; }

    public bool IsDead => hp <= 0;

    private void Start()
    {
        hp = maxHp;
        GameManager.Instance.portalHp.text = hp.ToString();

    }


    public void Dead()
    {
        GameManager.Instance.GameOver();
    }

    
}
