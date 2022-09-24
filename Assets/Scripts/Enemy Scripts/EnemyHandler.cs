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
        //Obtenemos los componentes necesarios y colocamos la vida maxima del enemigo
        enemyIA = GetComponent<EnemyIA>();
        actualHealth = stats.MaxHealth;
    }

    /// <summary>
    /// Calcula e inflige el daño que recibira el enemigo.
    /// </summary>
    /// <param name="damage"></param>
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
        }

        //Instanciamos el drop del enemigo
        if(actualHealth == 0 && !isDropSpawned)
        {
            Instantiate(drop,transform.position, Quaternion.identity);
            isDropSpawned = true;
        }

    }
}
