using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuggernautDrop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHandler>().CallInmortal(UpgradeSystem.instance.GetInmortalDuration());
            Destroy(gameObject);
        }
    }
}
