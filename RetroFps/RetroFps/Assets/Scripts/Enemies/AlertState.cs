using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyAI {

	EnemyStates enemy;
	float timer=0;

	//constructor function
	public AlertState(EnemyStates enemy)
	{
		this.enemy = enemy;
	}

	public void UpdateActions ()
	{
		Search ();
		Watch ();
		if(enemy.navMeshAgent.remainingDistance<=enemy.navMeshAgent.stoppingDistance)
			LookAround ();
	}

	void LookAround()
	{
		timer += Time.deltaTime;
		if (timer >= enemy.stayAlertTime) 
		{
			timer = 0;
			ToPatrolState ();
		}
	}

	void Watch()
	{
        if (enemy.EnemySpotted())
        {
            enemy.navMeshAgent.destination = enemy.lastKnownPosition;
            ToChaseState();

        }
	}

	void Search()
	{
		enemy.navMeshAgent.destination = enemy.lastKnownPosition;
		enemy.navMeshAgent.isStopped = false; //instead of navMeshAgent.Resume();???
	}

	public void OnTriggerEnter (Collision enemy)
	{

	}

	public void ToPatrolState ()
	{
		enemy.currentState = enemy.patrolState;
	}

	public void ToAttackState ()
	{
		Debug.Log ("Blad");
	}

	public void ToAlertState ()
	{
		Debug.Log ("Blad"); 
	}
	public void ToChaseState ()
	{
		enemy.currentState = enemy.chaseState;
	}
}

