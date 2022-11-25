using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class DeathZone : MonoBehaviour
{
    [Header("Death Zone Settings")]
    [SerializeField] Transform wayPointsContainer;
    private List<Transform> wayPoints;
    [SerializeField] private float waitTime;
    [SerializeField]private float damagePercentage;
    
    private List<Transform> shufflePoints;
    private int lastIndex = 0;
    private NavMeshAgent agent;
    private Transform actualTarget;
    private PlayerHandler player;
    private bool playerInside;

    [Header("Death Zone Levels")]
    public List<float> sizeLevels;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerHandler>();

        GetSpawnPoints();
        Shuffle();
        StartCoroutine(MoveZone());
    }

    private void Update()
    {
        if (player.isDead)
        {
            StopAllCoroutines();
            agent.enabled = false;
            enabled = false;
        }
    }

    protected void GetSpawnPoints()
    {
        wayPoints = new List<Transform>();
        foreach (Transform newSpawn in wayPointsContainer)
        {
            wayPoints.Add(newSpawn);
        }
    }

    public void ScaleDeathZone(int level)
    {
        float newSize = sizeLevels[level];
        transform.localScale = new Vector3(newSize, newSize, newSize);
    }

    private IEnumerator MoveZone()
    {
        //Debug.Log("Esperando nuevo destino...");
        yield return new WaitForSeconds(waitTime);
        GetTarget();
        yield return new WaitUntil(() => agent.remainingDistance <= 0);
        //Debug.Log("Iniciando Nuevo Ciclo");
        StartCoroutine(MoveZone());
    }

    private void GetTarget()
    {
        //Debug.Log("Nuevo Destino");
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
            player.TakeDamage(player.stats.MaxHealth * damagePercentage);
            yield return new WaitForSeconds(1.25f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            StartCoroutine(InflictWounds());
            //Debug.Log("Jugador Salio");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            StopCoroutine(InflictWounds());
            //Debug.Log("Jugador Adentro");
        }
    }
}
