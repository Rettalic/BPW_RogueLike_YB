using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public GameObject bullet;
    public Transform bulletSpawn;

    private bool canShoot = true;

    public int enemyHealth;
    public int enemyDamageGive;

    public LayerMask whatIsGround, whatIsPlayer;

    //patrolling
    public Vector3 walkPoint;
    public float walkPointRange;

    //states
    public float sightRange, attackrange;
    bool playerInsightRange, playerInAttackRange;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInsightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackrange, whatIsPlayer);

        if (playerInsightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange) AttackPlayer();

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            GoldPoints.Instance.dropGold(5);
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            EnemyTakeDamage(50);
        }
    }

    public void EnemyTakeDamage(int enemyDamageTake)
    {
        enemyHealth -= enemyDamageTake;
    }

    private void AttackPlayer()
    {
        transform.LookAt(player);
        Debug.Log("rattat");
        if (canShoot)
        {
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            canShoot = false;
            StartCoroutine(Shoot());

            agent.SetDestination(transform.position);
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.35f);
        canShoot = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackrange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
