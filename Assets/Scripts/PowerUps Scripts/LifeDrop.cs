using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDrop : MonoBehaviour
{
    public float healingPercentage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHandler player = other.GetComponent<PlayerHandler>();
            player.HealDamage(player.stats.MaxHealth * healingPercentage);
            Destroy(gameObject);
        }
    }
}
