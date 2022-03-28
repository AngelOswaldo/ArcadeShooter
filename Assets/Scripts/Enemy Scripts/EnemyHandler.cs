using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] private EnemyStats enemy;

    private float health;

    private void Start()
    {
        health = enemy.Health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
            Destroy(gameObject);
    }
    
}
