using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] target;
    private int currentTarget = 0;
    public float rangeTarget = 0.5f;
    public float sightRange = 20f;
    public float attackRange = 10f;
    public LayerMask layermasks;

    public float shootingCooldown;
    public float shootingTime;

    public Transform player;
    public enum stateEnum {Patrolling, Chasing, Attacking}
    private stateEnum state;



    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        if(SeePlayerAttackable() == true)
        {
            state = stateEnum.Attacking;
        }
        else if(SeePlayer() == true)
        {
            Debug.Log("chasing");
            state = stateEnum.Chasing;
        }
        else
        {
            Debug.Log("I am patrolling");
            state = stateEnum.Patrolling;
        }
        
        if(shootingTime >= 0)
        {
            shootingTime -= Time.deltaTime;
        }
        
        CheckState();

    }

    private void CheckState()
    {
        switch (state)
        {
            case stateEnum.Patrolling: Patrolling(); break;
            case stateEnum.Chasing: ChasePlayer(); break;
            case stateEnum.Attacking: AttackPlayer(); break;
        }
    }

    private void Patrolling()
    {
        agent.destination = target[currentTarget].position;

        if (Vector3.Distance(transform.position, agent.destination) < rangeTarget)
        {
            currentTarget++;
            if (currentTarget >= target.Length)
            {
                currentTarget = 0;
            }
        }
    }

   private bool SeePlayer()
    {
        Collider[] allColliders = Physics.OverlapSphere(agent.transform.position, sightRange, layermasks);
        foreach(Collider collider in allColliders) {
            if (collider.gameObject == player.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    private bool SeePlayerAttackable()
    {
        Collider[] allColliders = Physics.OverlapSphere(agent.transform.position, attackRange, layermasks);
        foreach (Collider collider in allColliders)
        {
            if (collider.gameObject == player.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        Debug.Log("also chasing the player");
    }
        
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void AttackPlayer()
    {
        if (shootingTime <= 0)
        {
            RaycastHit hit;
            Vector3 lookPosition = player.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
            lookPosition.y = agent.transform.position.y;
            agent.transform.LookAt(lookPosition);
            if (Physics.Raycast(agent.transform.position, transform.forward, out hit, attackRange, layermasks))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    Debug.Log("hitting player");
                    player.GetComponent<playerHealth>().TakeDamage(20);
                    shootingTime = shootingCooldown;
                }
            }
        }
    }
}
