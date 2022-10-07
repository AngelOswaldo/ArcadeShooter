using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuggernautDrop : MonoBehaviour
{
    public float duration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHandler>().CallInmortal(duration);
            Destroy(gameObject);
        }
    }
}
