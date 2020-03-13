using UnityEngine;
using System.Collections;

public class ChaseState : IEnemyAI
{

    EnemyStates enemy;

    public ChaseState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateActions()
    {

    }

    public void OnTriggerEnter(Collider enemy)
    {

    }

    public void ToPatrolState()
    {

    }

    public void ToAttackState()
    {

    }

    public void ToAlertState()
    {

    }

    public void ToChaseState()
    {

    }

}