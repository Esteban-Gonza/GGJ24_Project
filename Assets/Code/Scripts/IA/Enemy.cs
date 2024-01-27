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

    public Vector3 lastPosition;

    //states
    public float sightRange, attackRange;
    public bool bPlayerSeen;
    public bool bAttackPlayer;

    //scan variables
    [SerializeField]
    private float scanSpeed = 100f;
    private float scanRange = 45f;
    private float currentAngle = 0f;
    private float elapsedTime;


    private void Awake()
    {
        Player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        state = EnemyBehaviour.Patrol;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        Scan();

        if(bPlayerSeen)
        {
            state = EnemyBehaviour.LastPoint;
        }
        else
        {
            state = EnemyBehaviour.Patrol;
        }

        bAttackPlayer = Physics.CheckSphere(transform.position, attackRange, PlayerLayer);

        if (bAttackPlayer) 
        {
            state = EnemyBehaviour.Chase;
        }


        //State tree
        switch (state)
        {
            case EnemyBehaviour.Patrol:
                Patroling();
                break;

            case EnemyBehaviour.LastPoint:
                GoingToLastPoint();
                break;

            case EnemyBehaviour.Chase:
                Chasing();
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

    
    private void Chasing()
    {
        Debug.Log("chasing");
        agent.SetDestination(Player.position);
    }

    private void GoingToLastPoint()
    {

        if (lastPosition != Vector3.zero)
        {
            agent.SetDestination(lastPosition);
        }

        Vector3 distanceToLastPosition = transform.position - lastPosition;

        //Walkpoint Reached
        if (distanceToLastPosition.magnitude < 2f)
        {
            bPlayerSeen = false;
        }

        Debug.Log("IA: Last pos... " + distanceToLastPosition.magnitude);

    }

    //----------------------------- Functions ---------------------------
    #region

    void Scan()
    {
        // Rotar el rayo
        currentAngle = scanRange * Mathf.Sin(scanSpeed * elapsedTime);
        Quaternion rotation = Quaternion.Euler(0f, currentAngle, 0f);
        Vector3 direction = rotation * transform.forward;

        // Lanzar un rayo en la dirección del escaneo
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;


        //------------------------------- debug -----------------------------
        if (Physics.Raycast(ray, out hit, sightRange, PlayerLayer))
        {
            lastPosition = hit.transform.position;
            bPlayerSeen = true;
            // Realizar acciones basadas en la colisión con un obstáculo
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
        else
        {
            // Realizar acciones cuando no hay colisión
            Debug.DrawRay(ray.origin, ray.direction * sightRange, Color.green);
        }
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
    #endregion


    //----------------------------- debug---------------------------

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(lastPosition, 1f);

    }
}
