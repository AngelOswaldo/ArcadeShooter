using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPDrop : MonoBehaviour
{
    public int XPAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SystemExperience.instance.AddXP(XPAmount);
            Destroy(gameObject);
        }
    }
}
