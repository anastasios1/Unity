using UnityEngine;
using System.Collections;

public interface IEnemyAI
{

    void UpdateActions();

    void OnTriggerEnter(Collision enemy);

    void ToPatrolState();

    void ToAttackState();

    void ToAlertState();

    void ToChaseState();

}