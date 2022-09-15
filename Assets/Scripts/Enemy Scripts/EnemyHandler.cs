using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public EnemyStats stats;
    private EnemyIA enemyIA;

    [SerializeField] private int actualHealth;
    
    public GameObject drop;
    private bool isDropSpawned = false;
    private void Start()
    {
        enemyIA = GetComponent<EnemyIA>();
        actualHealth = stats.MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        if(actualHealth < damage)
        {
            actualHealth = 0;
            GameManager.instance.actualEnemies.Remove(this);
            enemyIA.DeathState();
        }
        else
        {
            actualHealth-=damage;
            StartCoroutine(enemyIA.HittedState());
        }

        if(actualHealth == 0 && !isDropSpawned)
        {
            Instantiate(drop,transform.position, Quaternion.identity);
            isDropSpawned = true;
        }

    }
}
