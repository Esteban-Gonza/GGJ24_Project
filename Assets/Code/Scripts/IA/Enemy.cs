using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // public Animator animator;
    public LayerMask groundLayer, PlayerLayer;

    [SerializeField]
    private EnemyBehaviour state;
    private NavMeshAgent agent;

    [SerializeField]
    private Transform Player;

    //patroling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool bIsPlayerInSightRange, bIsPlayerInAttackRange, bAttackPlayer, bHasHeardPLayer;


    private void Awake()
    {
        Player = GameObject.Find("PlayerObject").transform;
        agent = GetComponent<NavMeshAgent>();
        state = EnemyBehaviour.Patrol;
    }

    private void Update()
    {
        //Check for sight and attack range
        bIsPlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, PlayerLayer);
        bIsPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerLayer);


        if(!bIsPlayerInSightRange)
        {
            state = EnemyBehaviour.Patrol;
        }

        if(bIsPlayerInAttackRange)
        {
            state = EnemyBehaviour.ActiveChase;
            if(bIsPlayerInAttackRange)
            {
                state = EnemyBehaviour.PasiveChase;
            }
        }

        //State tree
        switch (state)
        {
            case EnemyBehaviour.PasiveChase:
                PasiveChasing();
                break;

            case EnemyBehaviour.ActiveChase:
                ActiveChasing();
                break;

            case EnemyBehaviour.Patrol:
                Patroling();
                break;

            case EnemyBehaviour.RunAway:
                RunAway();
                break;

            default:
                break;
        }

        // animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            walkPoint = GetRandonWaypoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint Reached
        if(distanceToWalkPoint.magnitude < 2f)
        {
            walkPointSet = false;
        }

        Debug.Log("IA: Patroling... " + distanceToWalkPoint.magnitude);

    }

    private Vector3 GetRandonWaypoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        Vector3 RandomWalkPoint = new Vector3(
            transform.position.x + randomX,
            transform.position.y,
            transform.position.z + randomZ
        );

        if (Physics.Raycast(RandomWalkPoint, -transform.up, 8f, groundLayer))
        {
            Debug.DrawRay(RandomWalkPoint, -transform.up * 8f, Color.green);
            walkPointSet = true;
            return RandomWalkPoint;
        }
        else
        {
            Debug.DrawRay(RandomWalkPoint, -transform.up * 2, Color.red);
            return walkPoint;
        }
    }
    private void ActiveChasing()
    {
        Debug.Log("chasing");
        agent.SetDestination(Player.position);
    }

    private void PasiveChasing()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(Player);
    }

    private void RunAway()
    {
        //Deactivate Sight
        //Patroling();
    }

    private void AttackPlayer()
    {
        Debug.Log("attacking");
        agent.SetDestination(Player.position);
        //transform.LookAt(Player);

        // animator.SetTrigger("Attack");

        // if (!alreadyAttacked)
        // {
        //     //Attack code 
        //     
        //
        //     alreadyAttacked = true;
        //     Invoke(nameof(ResetAttack), timeBetweenAttacks);
        // }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == PlayerLayer)
        {
            //TakeDamage(15);
        }
    }

    //----------------------------- debug---------------------------

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
