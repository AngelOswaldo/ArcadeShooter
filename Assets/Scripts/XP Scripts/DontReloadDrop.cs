using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontReloadDrop : MonoBehaviour
{
    public float duration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHandler>().CallDontReload(duration);
            Destroy(gameObject);
        }
    }
}
