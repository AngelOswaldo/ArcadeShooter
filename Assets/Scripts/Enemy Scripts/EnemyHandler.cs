using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public EnemyStats stats;
    private EnemyIA enemyIA;

    [SerializeField] private int actualHealth;
    
    public GameObject[] drops;

    private bool isDropSpawned = false;
    private void Start()
    {
        //Obtenemos los componentes necesarios y colocamos la vida maxima del enemigo
        enemyIA = GetComponent<EnemyIA>();
        actualHealth = stats.GetMaxHealth(GameManager.instance.ActualWave());
    }

    /// <summary>
    /// Calcula e inflige el daño que recibira el enemigo.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if(actualHealth <= damage)
        {
            actualHealth = 0;
            GameManager.instance.actualEnemies.Remove(this);
            enemyIA.DeathState();
        }
        else
        {
            actualHealth-=damage;
        }

        //Instanciamos el drop del enemigo
        if(actualHealth <= 0 && !isDropSpawned)
        {
            isDropSpawned = true;
            Invoke(nameof(DropItem), stats.DeathAnimation);
        }
    }

    private void DropItem()
    {
        int randomPrice = Random.Range(1, 101);

        if(randomPrice >= 1 && randomPrice <= 55)
        {
            return;            
        }
        else if(randomPrice >= 56 && randomPrice <= 90)
        {
            Instantiate(drops[0], transform.position + new Vector3(0f, 1.25f, 0f), Quaternion.identity);            
        }
        else if(randomPrice >= 91 && randomPrice <= 95)
        {
            Instantiate(drops[1], transform.position + new Vector3(0f, 1.25f, 0f), Quaternion.identity);            
        }
        else
        {
            Instantiate(drops[2], transform.position + new Vector3(0f, 1.25f, 0f), Quaternion.identity);
        }
    }
}
