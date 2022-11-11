using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesSelector : MonoBehaviour
{
    public List<GameObject> upgrades;
    [SerializeField] private int upgradesToShow;

    private void OnEnable()
    {
        CheckList();
        ShowUpgrades();
    }

    private void CheckList()
    {
        for(int i = 0; i < upgrades.Count; i++)
        {
            if(upgrades[i] == null)
            {
                upgrades.RemoveAt(i);
            }
        }
    }


    private void ShowUpgrades()
    {
        foreach(GameObject upgrade in upgrades)
        {
            upgrade.SetActive(false);
        }

        int a, b, c;
        int maxNumbers = upgrades.Count;
        do
        {
            a = Random.Range(0, maxNumbers);
            b = Random.Range(0, maxNumbers);
            c = Random.Range(0, maxNumbers);
        } while ((a == b) || (b == c) || (a == c));
        //Debug.Log(a + "," + b + "," + c);
        upgrades[a].SetActive(true);
        upgrades[b].SetActive(true);
        upgrades[c].SetActive(true);

    }
}
