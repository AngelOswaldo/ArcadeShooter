using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class DeathZone : MonoBehaviour
{
    public List<Transform> wayPoints;
    public List<Transform> shufflePoints;
    private int lastIndex;

    private NavMeshAgent agent;
    private Transform actualTarget;

    private PlayerHandler player;

    public float waitTime;
    public bool playerInside;
    public int damage;
    private bool isMoving = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        lastIndex = -1;

        player = FindObjectOfType<PlayerHandler>();

        Shuffle();
        StartCoroutine(MoveZone());
    }

    private void Update()
    {
        //Debug.Log(agent.velocity);
        if (!player.isDead)
        {
            float distance = agent.remainingDistance;
            if (agent.pathStatus == NavMeshPathStatus.PathComplete && distance == 0)
            {
                Debug.Log("I arrived");
                StartCoroutine(MoveZone());
            }

            else return;

            //if (Vector3.Distance(agent.destination,transform.position) < .05f )
            //{
            //    StartCoroutine(MoveZone());       
            //}
            //else
            //{
            //    return;
            //}
        }
    }

    private IEnumerator MoveZone()
    {
        if(agent.pathStatus != NavMeshPathStatus.PathComplete)
            yield return null;

        if (agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            Debug.Log("Esperando nuevo destino...");
            yield return new WaitForSeconds(waitTime);
            GetTarget();
        }
    }

    private void GetTarget()
    {
        Debug.Log("Nuevo Destino");
        agent.isStopped = false;
       
        if (lastIndex < shufflePoints.Count)
        {
            lastIndex++;
        }
        else
        {
            Shuffle();
            lastIndex = 0;
        }
        actualTarget = shufflePoints[lastIndex];
        agent.SetDestination(actualTarget.position);
    }

    private void Shuffle()
    {
        System.Random random = new System.Random();
        shufflePoints = wayPoints.OrderBy(_ => random.Next()).ToList();
    }

    private IEnumerator InflictWounds()
    {
        while (!playerInside)
        {
            player.TakeDamage(damage);
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            StartCoroutine(InflictWounds());
            Debug.Log("Jugador Salio");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            StopCoroutine(InflictWounds());
            Debug.Log("Jugador Adentro");
        }
    }
}
