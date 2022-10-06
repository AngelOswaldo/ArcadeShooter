using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemExperience : MonoBehaviour
{
    private int actualXP = 0;
    private int expectedXP = 0;
    [SerializeField] private int NextLevelXP;

    public static SystemExperience instance;

    [SerializeField] private GameObject UIUpgrades;
    [SerializeField] private Transform weaponHolder;
    public WeaponSwitching weaponSwitching;

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
        expectedXP = NextLevelXP;
        NewUpgrade();
    }

    public void AddXP(int amount)
    {
        actualXP += amount;
        if(actualXP >= expectedXP)
        {
            actualXP = 0;
            if(expectedXP >= 2)
            {
                Debug.Log("Maximo nivel alcanzado...");
            }
            else
            {
                expectedXP += NextLevelXP;
                NewUpgrade();
            }
        }
    }

    private void NewUpgrade()
    {
        Time.timeScale = 0f;
        UIUpgrades.SetActive(true);
    }

    public GameObject currentWapon;

    public void UnlockWeapon(GameObject weapon)
    {
        if(currentWapon != null)
            currentWapon.SetActive(false);
        weapon.transform.SetParent(weaponHolder);
        weapon.SetActive(true);
        currentWapon = weapon;
        Time.timeScale = 1;
        UIUpgrades.SetActive(false);
    }
}
