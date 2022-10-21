using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemExperience : MonoBehaviour
{
    private int actualXP = 0;

    public static SystemExperience instance;

    [SerializeField] private GameObject UIUpgrades;
    [SerializeField] private Transform weaponHolder;
    [HideInInspector] public GameObject currentWeapon;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        NewUpgrade();
    }

    public void AddXP(int amount)
    {
        actualXP =+ amount;
    }

    private void NewUpgrade()
    {
        Time.timeScale = 0f;
        UIUpgrades.SetActive(true);
    }

    public void UnlockWeapon(GameObject weapon)
    {
        if(currentWeapon != null)
            currentWeapon.SetActive(false);
        weapon.transform.SetParent(weaponHolder);
        weapon.SetActive(true);
        currentWeapon = weapon;
        Time.timeScale = 1;
        UIUpgrades.SetActive(false);
    }
}
