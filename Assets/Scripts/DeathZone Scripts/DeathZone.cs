using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class DeathZone : MonoBehaviour
{
    public List<Transform> wayPoints;
    public List<Transform> shufflePoints;
    private int lastIndex = 0;

    private NavMeshAgent agent;
    private Transform actualTarget;

    private PlayerHandler player;

    public float waitTime;
    public bool playerInside;
    public int damage;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerHandler>();

        Shuffle();
        StartCoroutine(MoveZone());
    }

    private void Update()
    {
        if (player.isDead)
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator MoveZone()
    {
        Debug.Log("Esperando nuevo destino...");
        yield return new WaitForSeconds(waitTime);
        GetTarget();
        yield return new WaitUntil(() => agent.remainingDistance <= 0);
        Debug.Log("Iniciando Nuevo Ciclo");
        StartCoroutine(MoveZone());
    }

    private void GetTarget()
    {
        Debug.Log("Nuevo Destino");
        agent.isStopped = false;

        if (lastIndex < shufflePoints.Count - 1)
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
