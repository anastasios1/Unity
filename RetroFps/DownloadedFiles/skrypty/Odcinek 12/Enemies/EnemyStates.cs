using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyStates : MonoBehaviour
{
    public Transform[] waypoints;
    public int patrolRange;
    public int attackRange = 1;
    public Transform vision;

    [HideInInspector]
    public AlertState alertState;
    [HideInInspector]
    public AttackState attackState;
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public PatrolState patrolState;
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
        // Tworzymy instancje każdego ze stanu
        // I przekazujemy do nich obiekt EnemyStates
        alertState = new AlertState(this);
        attackState = new AttackState(this);
        chaseState = new ChaseState(this);
        patrolState = new PatrolState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        // Przypisujemy startowy stan
        currentState = patrolState;
    }

    void Update()
    {
        // Co klatke gry wykonujemy akcje aktualnego stanu
        currentState.UpdateActions();
    }

    void OnTriggerEnter(Collider otherObj)
    {
        // Po wejściu w interakcje z innym obiektem
        // Wywolaj funkcje OnTriggerEnter zgodna z aktualnym stanem
        currentState.OnTriggerEnter(otherObj);
    }
}