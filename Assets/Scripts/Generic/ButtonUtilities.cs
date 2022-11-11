using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUtilities : MonoBehaviour
{
    [SerializeField] private UpgradesSelector upgradesSelector;
    [SerializeField] Transform newParent;

    [Header("Button Settings")]
    public int clicksToDestroy;
    private int clicksCount;

    public void DestroyButton()
    {
        clicksCount += 1;
        if(clicksCount >= clicksToDestroy)
        {
            upgradesSelector.upgrades.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public void SetParent(GameObject child)
    {
        child.transform.SetParent(newParent);
        newParent.GetComponent<UpgradesSelector>().upgrades.Add(child);
    }
}
