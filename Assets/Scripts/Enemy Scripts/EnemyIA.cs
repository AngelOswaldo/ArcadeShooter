using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform target;
    private PlayerHandler player;

    private EnemyHandler mHandler;
    private bool canAttack = true;
    private bool isDead = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mHandler = GetComponent<EnemyHandler>();

        agent.speed = mHandler.stats.MovementSpeed;
        agent.stoppingDistance = mHandler.stats.ChaseDistance;

        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = target.GetComponent<PlayerHandler>();
    }

    private void Update()
    {
        if (!player.isDead)
        {
            IAStateMachine();
        }
        else
        {
            IdleState();
        }
    }

    private void IAStateMachine()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (!canAttack)
            return;

        if (!isDead && canAttack)
        {
            if (distance <= mHandler.stats.ChaseDistance)
            {
                AttackState();
            }
            else if(distance > mHandler.stats.ChaseDistance)
            {
                ChaseState();
            }
        }
    }

    private void AttackState()
    {
        agent.isStopped = true;

        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), mHandler.stats.RotationSpeed * Time.deltaTime);
        StartCoroutine(Attack());
        Debug.Log("Atacando");
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        yield return new WaitForSeconds(mHandler.stats.SpeedAttack);
        player.TakeDamage(mHandler.stats.DamageAmount);
        canAttack = true;
    }

    private void ChaseState()
    {
        agent.isStopped = false;
        agent.SetDestination(target.position);
    }

    public void DeathState()
    {
        isDead = true;
        agent.isStopped = true;
        Destroy(gameObject);
    }

    public IEnumerator HittedState()
    {
        agent.isStopped = true;
        canAttack = false;
        yield return new WaitForSeconds(mHandler.stats.HitDuration);
        agent.isStopped = false;
        canAttack = true;
        Debug.Log("Me Hirieron");
    }

    private void IdleState()
    {
        canAttack = false;
        agent.isStopped = true;
    }
}
