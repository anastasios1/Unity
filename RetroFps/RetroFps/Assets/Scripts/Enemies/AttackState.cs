using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyAI {

	EnemyStates enemy;
	float timer;

	//constructor function
	public AttackState(EnemyStates enemy)
	{
		this.enemy = enemy;
	}

	public void UpdateActions ()
	{
		timer += Time.deltaTime;
		float distance = Vector3.Distance (enemy.chaseTarget.transform.position, enemy.transform.position);

		if (distance > enemy.attackRange && enemy.onlyMelee == true) 
		{
			ToChaseState ();
		}

		if (distance > enemy.shootRange && enemy.onlyMelee == false)
		{
			
			ToChaseState ();

		}

		Watch ();

		if (distance <= enemy.shootRange && distance > enemy.attackRange && enemy.onlyMelee == false && timer>=enemy.attackDelay) 
		{
			Attack (true);
			timer = 0;
		}
		if (distance <= enemy.attackRange && timer >= enemy.attackDelay) 
		{
			Attack (false);
			timer = 0;

		}

	}

	void Attack(bool shoot)
	{
		if (shoot == false) {
            Debug.Log("That's A meele attack");
			enemy.chaseTarget.SendMessage ("EnemyHit", enemy.meleeDamage, SendMessageOptions.DontRequireReceiver);
		}
		else if (shoot == true) 
		{
            Debug.Log("That's A range Atack");
			GameObject missile=GameObject.Instantiate (enemy.missile, enemy.transform.position, Quaternion.identity);
			missile.GetComponent<Missile> ().speed = enemy.missileSpeed;
			missile.GetComponent<Missile>().damage = enemy.missileDamage;
            //enemy.chaseTarget.SendMessage("EnemyHit", enemy.missileDamage, SendMessageOptions.DontRequireReceiver);
        }

	}

	void Watch()
	{
        if (!enemy.EnemySpotted())
        {
            ToAlertState();
        }
	}


	public void OnTriggerEnter (Collision enemy)
	{

	}

	public void ToPatrolState ()
	{
	}

	public void ToAttackState ()
	{

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

