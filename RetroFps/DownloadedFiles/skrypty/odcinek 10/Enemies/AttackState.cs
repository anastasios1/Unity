using UnityEngine;
using System.Collections;

public class AttackState : IEnemyAI
{

    EnemyStates enemy;

    public AttackState(EnemyStates enemy)
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