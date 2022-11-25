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
    private bool isFinished = false;

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
        UIManager.instance.UpdateWaveCount(waveIndex + 1);
    }

    private void Update()
    {
        if (player.isDead)
        {
            StopAllCoroutines();
            UIManager.instance.SetGameOverUI();
            ScoreManager.instance.GetNewHighScore();
            enabled = false;
            //Debug.Log("El jugador murio...");
        }

        if (isFinished)
            return;

        if (isAllSpawning && actualEnemies.Count <= 0)
        {
            isFinished = true;
        }

        if (isFinished)
        {
            if(waveIndex < waves.Count)
                waveIndex += 1;

            UIManager.instance.UpdateWaveCount(waveIndex + 1);

            if (waveIndex % 2 == 0)
            {
                if (waveIndex == 4)
                {
                    UpgradeSystem.instance.NewWeapon();
                }
            }
            else
            {
                UpgradeSystem.instance.NewUpgrade();
            }

            if (waveIndex > waves.Count)
            {
                //Debug.Log("Juego Terminado...");
                UIManager.instance.SetGameOverUI();
                return;
            }
            StartCoroutine(SpawnWave());
            isFinished = false;

        }
    }

    protected IEnumerator SpawnWave()
    {
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
