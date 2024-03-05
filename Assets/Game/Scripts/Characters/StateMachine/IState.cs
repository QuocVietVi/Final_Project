using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnter(AllyAndEnemy allyAndEnemy);

    void OnExcute(AllyAndEnemy allyAndEnemy);

    void OnExit(AllyAndEnemy allyAndEnemy);
}

