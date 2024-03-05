using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class MoveState : IState
{
    public void OnEnter(AllyAndEnemy allyAndEnemy)
    {
    }

    public void OnExcute(AllyAndEnemy allyAndEnemy)
    {
        if (allyAndEnemy.target != null)
        {

            if (allyAndEnemy.TargetInAttackRange())
            {
                allyAndEnemy.ChangeState(new AttackState());
            }
            else
            {
                allyAndEnemy.Move();

            }

        }
        else
        {
            allyAndEnemy.Move();
        }
    }

    public void OnExit(AllyAndEnemy allyAndEnemy)
    {
    }
}
