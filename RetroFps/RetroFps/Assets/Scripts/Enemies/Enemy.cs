using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public Sprite deadBody;
	public int maxHealth;
	public float health;

   

	EnemyStates es;
	NavMeshAgent nma;
	SpriteRenderer sr;
	BoxCollider bx;

	private void Start()
	{
		health = maxHealth;
		es = GetComponent<EnemyStates> ();
		nma = GetComponent<NavMeshAgent> (); 
		sr = GetComponent<SpriteRenderer> ();
		bx = GetComponent<BoxCollider> ();
	}

	private void Update()
	{
		if (health <= 0) 
		{
			es.enabled = false;
			nma.enabled = false;
			sr.sprite = deadBody;
			bx.center = new Vector3 (0, -0.8f, 0);
			bx.size = new Vector3 (1.05f, 0.43f, 0.2f);
		}
	}

 	void AddDamage(int damage)
	{
		health = health - damage;
	}



}
