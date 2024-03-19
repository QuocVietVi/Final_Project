using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;
    public void OnEnter(AllyAndEnemy allyAndEnemy)
    {
        if (allyAndEnemy.target != null || LevelManager.Instance.map.tower != null)
        {
            allyAndEnemy.StopMoving();
            allyAndEnemy.Attack();
        }
        timer = 0;
    }

    public void OnExcute(AllyAndEnemy allyAndEnemy)
    {
        timer += Time.deltaTime;
        if (timer >= 1f && allyAndEnemy.AEType == AllyAndEnemyType.CloseRange)
        {
            allyAndEnemy.ChangeState(new MoveState());
        }
        if (timer >= 1.5f && allyAndEnemy.AEType == AllyAndEnemyType.LongRange)
        {
            allyAndEnemy.ChangeState(new MoveState());
        }
        if (timer >= 2f && allyAndEnemy.AEType == AllyAndEnemyType.Healing)
        {
            allyAndEnemy.ChangeState(new MoveState());
        }

    }

    public void OnExit(AllyAndEnemy allyAndEnemy)
    {
    }
}
