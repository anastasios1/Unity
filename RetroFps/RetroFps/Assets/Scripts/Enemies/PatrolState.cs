﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyAI {

	EnemyStates enemy;
	int nextWayPoint=0;

	//constructor function
	public PatrolState(EnemyStates enemy)
	{
		this.enemy = enemy;
	}

	public void UpdateActions ()
	{
		Watch ();
		Patrol ();
	}

	void Watch()
	{
        if (enemy.EnemySpotted())
        {
            Debug.Log("EnemySpotted");
            ToChaseState();
        }
	}

	void Patrol()
	{
		enemy.navMeshAgent.destination = enemy.waypoints[nextWayPoint].position;
		enemy.navMeshAgent.isStopped = false;
		if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending) 
		{
			nextWayPoint =(nextWayPoint + 1)%enemy.waypoints.Length;
		}
		
	}

	public void OnTriggerEnter (Collision enemy)
	{
		if (enemy.gameObject.CompareTag ("Player")) 
		{
			ToAlertState ();
		}
	}

	public void ToPatrolState ()
	{
		Debug.Log ("I am patroliing aleady!!!");
	}

	public void ToAttackState ()
	{
		enemy.currentState = enemy.attackState;
	}

	public void ToAlertState ()
	{
		enemy.currentState = enemy.alertState;
		
	}

	public void ToChaseState ()
	{
		enemy.currentState = enemy.chaseState;
		
	}
}
