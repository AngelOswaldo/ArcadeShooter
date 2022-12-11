using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradesSelector : MonoBehaviour
{
    public List<GameObject> upgrades;
    [SerializeField] private int upgradesToShow;

    public Button input1, input2, input3;
    public TextMeshProUGUI inputText1, inputText2, inputText3;

    private void OnEnable()
    {
        CheckList();
        ShowUpgrades();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            input1.onClick.Invoke();
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            input2.onClick.Invoke();
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            input3.onClick.Invoke();
        }
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
        upgrades[a].transform.SetSiblingIndex(0);
        upgrades[b].SetActive(true);
        upgrades[b].transform.SetSiblingIndex(1);
        upgrades[c].SetActive(true);
        upgrades[c].transform.SetSiblingIndex(2);

        input1 = upgrades[a].GetComponent<Button>();
        input2 = upgrades[b].GetComponent<Button>();
        input3 = upgrades[c].GetComponent<Button>();

        inputText1 = upgrades[a].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        inputText2 = upgrades[b].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        inputText3 = upgrades[c].transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        SetButtonText();
    }

    private void SetButtonText()
    {
        inputText1.text = "Presiona 1.";
        inputText2.text = "Presiona 2.";
        inputText3.text = "Presiona 3.";
    }
}
