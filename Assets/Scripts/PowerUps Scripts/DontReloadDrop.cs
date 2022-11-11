using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontReloadDrop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHandler>().CallDontReload(UpgradeSystem.instance.GetDontReloadDuration());
            Destroy(gameObject);
        }
    }
}
