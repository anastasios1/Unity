using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour {

	public Transform[] waypoints;
	public int shootRange;
	public int patrolRange;
	public int attackRange;
	public Transform vision;
	public float stayAlertTime;

    public float viewAngle;

	public GameObject missile;
	public float missileDamage;
	public float missileSpeed;

	public bool onlyMelee=false;
	public float meleeDamage;
	public float attackDelay;

    public LayerMask raycastMask;

	[HideInInspector]
	public AlertState alertState;
	[HideInInspector]
	public PatrolState patrolState;
	[HideInInspector]
	public AttackState attackState;
	[HideInInspector]
	public ChaseState chaseState;
	[HideInInspector]
	public IEnemyAI currentState;
	[HideInInspector]
	public NavMeshAgent navMeshAgent;
	[HideInInspector]
	public Transform chaseTarget;
	[HideInInspector]
	public Vector3 lastKnownPosition;

	void Awake()
	{
		alertState=new AlertState(this);
		patrolState= new PatrolState(this);
		attackState=new AttackState(this);
		chaseState=new ChaseState(this);

		navMeshAgent = GetComponent<NavMeshAgent> ();

	}

	// Use this for initialization
	void Start () {

		currentState = patrolState;
	}
	
	// Update is called once per frame
	void Update () {

		currentState.UpdateActions ();
	}

	void OnTriggerEnter(Collision otherObj)
	{
		currentState.OnTriggerEnter (otherObj);
	}

	void HiddenShot(Vector3 shotPosition)
	{
		//Debug.Log ("Someone is Shooting!!!");
		lastKnownPosition = shotPosition;
		currentState = alertState;
	}

    public bool EnemySpotted()
    {
        Vector3 direction = GameObject.FindWithTag("Player").transform.position - transform.position;
        float angle = Vector3.Angle(direction, vision.forward);

        if (angle < viewAngle * 0.5)
        {
            RaycastHit hit;
            if (Physics.Raycast(vision.transform.position, direction.normalized, out hit, patrolRange, raycastMask))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    chaseTarget = hit.transform;
                    lastKnownPosition = hit.transform.position;
                    return true;
                }
            }
        }
        return false;
    }
}
