using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : AllyAndEnemy
{

    protected override void Dead()
    {
        base.Dead();
        Invoke(nameof(Victory), 1f);
    }

    private void Victory()
    {
        GameManager.Instance.Victory();
    }
}
