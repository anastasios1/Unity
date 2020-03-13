using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyAI {

	EnemyStates enemy;

	//constructor function
	public ChaseState(EnemyStates enemy)
	{
		this.enemy = enemy;
		
	}

	public void UpdateActions ()
	{
		Watch ();
		Chase ();
	}

	void Watch()
	{
        if (enemy.EnemySpotted())
        {
            ToAlertState();

        }
			
	}

	void Chase()
	{
		enemy.navMeshAgent.destination = enemy.chaseTarget.position;
		enemy.navMeshAgent.isStopped = false;
		if (enemy.navMeshAgent.remainingDistance <= enemy.attackRange && enemy.onlyMelee == true) {

			enemy.navMeshAgent.isStopped =true; //instead of enemy.navMeshAgent.Stop();
			ToAttackState ();
		} 
		else if(enemy.navMeshAgent.remainingDistance<=enemy.shootRange && enemy.onlyMelee==false)
		{
			enemy.navMeshAgent.isStopped = true;
			ToAttackState ();
		}
	}

	public void OnTriggerEnter (Collision enemy)
	{

	}

	public void ToPatrolState ()
	{
		Debug.Log ("Cann't Do That");
	}

	public void ToAttackState ()
	{
		Debug.Log ("Attack State");
		enemy.currentState = enemy.attackState;
	}

	public void ToAlertState ()
	{
		Debug.Log ("To Alert State");
		enemy.currentState = enemy.alertState;
	}
	public void ToChaseState ()
	{
		Debug.Log ("Cann't Do That");
	}
}

