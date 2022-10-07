using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Spawning Settings")]
    [SerializeField] private List<Wave> waves;
    [SerializeField] private Transform spawnsContainer;
    private List<Transform> spawnPoints;
    private int waveIndex = 0;

    [HideInInspector] public List<EnemyHandler> actualEnemies;
    private bool isAllSpawning = false;
    [HideInInspector] public bool isFinished = false;

    [Header("Others")]
    public PlayerHandler player;
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GetSpawnPoints();
        StartCoroutine(SpawnWave());
    }

    private void Update()
    {
        if (player.isDead)
        {
            StopAllCoroutines();
            UIManager.instance.SetLoseUI();
            Debug.Log("El jugador murio...");
        }

        if (isFinished)
            return;

        if (isAllSpawning && actualEnemies.Count <= 0)
        {
            isFinished = true;
        }

        if (isFinished)
        {
            waveIndex += 1;
            if (waveIndex > waves.Count - 1)
            {
                Debug.Log("Juego Terminado...");
                UIManager.instance.SetVictoryUI();
                return;

            }
            StartCoroutine(SpawnWave());
            isFinished = false;

        }
    }

    protected IEnumerator SpawnWave()
    {
        UIManager.instance.UpdateWaveCount(waveIndex + 1);

        int spawnIndex;
        Transform selectedSpawn;

        Wave newWave = waves[waveIndex];

        for (int i = 0; i < newWave.Enemies.Count; i++)
        {
            spawnIndex = Random.Range(0, spawnPoints.Count);
            selectedSpawn = spawnPoints[spawnIndex];

            GameObject newEnemy = Instantiate(newWave.Enemies[i], selectedSpawn.position, Quaternion.identity);
            actualEnemies.Add(newEnemy.GetComponent<EnemyHandler>());

            yield return new WaitForSeconds(newWave.SpawnSpeed);
        }

        isAllSpawning = true;
    }

    protected void GetSpawnPoints()
    {
        spawnPoints = new List<Transform>();
        foreach (Transform newSpawn in spawnsContainer)
        {
            spawnPoints.Add(newSpawn);
        }
    }

    public int ActualWave()
    {
        return waveIndex;
    }
}
