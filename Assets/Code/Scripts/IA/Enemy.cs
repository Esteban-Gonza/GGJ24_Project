using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // public Animator animator;
    public LayerMask groundLayer, PlayerLayer;

    private EnemyBehaviour state;
    private NavMeshAgent agent;

    //patroling
    private Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool bIsPlayerInSightRange, bIsPlayerInAttackRange, bIsHiding;

    //waypoints 
    public Transform[] waypoints;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        bIsPlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, PlayerLayer);
        bIsPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerLayer);



        if (!bIsPlayerInSightRange && !bIsPlayerInAttackRange)
        {
            //Patroling();
            state = EnemyBehaviour.Patrol;
        }

        if (bIsPlayerInSightRange && !bIsPlayerInAttackRange)
        {
            ActiveChasing();
        }

        if (bIsPlayerInAttackRange && bIsPlayerInSightRange)
        {
            AttackPlayer();
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

            case EnemyBehaviour.Explore:
                Explore();
                break;

            case EnemyBehaviour.RunAway:
                RunAway();
                break;

            default:
                break;
        }

        // animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void PasiveChasing()
    {
        throw new System.NotImplementedException();
    }

    private void Patroling()
    {
        throw new System.NotImplementedException();
    }

    public void init(EnemyBehaviour state)
    {
        this.state = state;
    }

    //----------------------------- funcitons ---------------------------

    Vector3 GetRandonWaypoint()
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

    public void AssignTask(EnemyBehaviour inState)
    {
        state = inState;
    }
    //----------------------------- states ---------------------------

    private void Explore()
    {
        // if (!walkPointSet) GetRandonWaypoint();
        walkPoint = waypoints[0].position;

        // if (walkPointSet)
        // {
        agent.SetDestination(walkPoint);
        //}
        // Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // //walkPoint reached
        // if (distanceToWalkPoint.magnitude < 7f)
        // {
        //     walkPointSet = false;
        // }

    }

    private void Farm()
    {

    }

    private void RunAway()
    {
        walkPoint = queen.position;
        agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkPoint reached
        if (distanceToWalkPoint.magnitude < 3f)
        {
            state = AntBehaviour.waitForInstructions;
        }
    }

    private void ActiveChasing()
    {
        Debug.Log("chasing");
        // agent.SetDestination(player.position);
        walkPointSet = false;
    }

    private void AttackPlayer()
    {
        Debug.Log("attacking");
        //the sure enemy doesn't move
        agent.SetDestination(transform.position);

        // animator.SetTrigger("Attack");

        // Vector3 playerLookAt = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        // transform.LookAt(playerLookAt);

        // if (!alreadyAttacked)
        // {
        //     //Attack code 
        //     Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        //     rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        //     rb.AddForce(transform.up * 8f, ForceMode.Impulse);

        //     //
        //     alreadyAttacked = true;
        //     Invoke(nameof(ResetAttack), timeBetweenAttacks);
        // }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //----------------------------- debug ---------------------------

    public void TakeDamage(int inDamage)
    {
        health -= inDamage;

        if (health <= 0)
        {
            bIsHiding = false;
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == PlayerLayer)
        {
            TakeDamage(15);
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
