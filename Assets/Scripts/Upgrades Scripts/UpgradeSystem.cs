using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public static UpgradeSystem instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject UIWeapons;
    [SerializeField] private GameObject UIUpgrades;
    [SerializeField] private Transform weaponHolder;
    [HideInInspector] public GameObject currentWeapon;

    [Header("Upgrades")]
    [SerializeField] private PlayerHandler player;
    [SerializeField] private DeathZone deathZone;
    [SerializeField] private List<float> dontReloadLevels;
    [SerializeField] private List<float> inmortalLevels;
    [SerializeField] private WeaponHandler shotgun;
    [SerializeField] private WeaponHandler rifle;
    [SerializeField] private WeaponHandler submachine;
    [SerializeField] private WeaponHandler pistol;
    [SerializeField] private WeaponHandler sniper;

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
        NewWeapon();
        UpgradePlayerStats();
        UpgradeShotgun();
        UpgradeRifle();
        UpgradeSubmachine();
        UpgradePistol();
        UpgradeSniper();
    }

    public void NewUpgrade()
    {
        Time.timeScale = 0f;
        UIUpgrades.SetActive(true);
    }

    public void NewWeapon()
    {
        Time.timeScale = 0f;
        UIWeapons.SetActive(true);
    }

    public void ResetTime(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void UnlockWeapon(GameObject weapon)
    {
        if(currentWeapon != null)
            currentWeapon.SetActive(false);
        weapon.transform.SetParent(weaponHolder);
        weapon.SetActive(true);
        currentWeapon = weapon;
    }

    private int actualPlayerUpgrade = -1;
    public void UpgradePlayerStats()
    {
        if(actualPlayerUpgrade < player.stats.HealthLevels.Count -1)
            actualPlayerUpgrade += 1;

        player.stats.MaxHealth = player.stats.HealthLevels[actualPlayerUpgrade];
        player.stats.WalkSpeed = player.stats.WalkSpeedLevels[actualPlayerUpgrade];
        player.stats.RunSpeed = player.stats.RunSpeedLevels[actualPlayerUpgrade];
        player.HealDamage(player.stats.MaxHealth);
    }

    private int actualDeathZoneUpgrade = 0;
    public void UpgradeDeathZone()
    {
        if (actualDeathZoneUpgrade < deathZone.sizeLevels.Count - 1)
            actualDeathZoneUpgrade += 1;

        deathZone.ScaleDeathZone(actualDeathZoneUpgrade);
    }

    private int actualDontReloadUpgrade = 0;
    public void UpgradeDontReload()
    {
        if (actualDontReloadUpgrade < dontReloadLevels.Count - 1)
            actualDontReloadUpgrade += 1;
    }
    public float GetDontReloadDuration()
    {
        return dontReloadLevels[actualDontReloadUpgrade];
    }

    private int actualInmortalUpgrade = 0;
    public void UpgradeInmortal()
    {
        if (actualInmortalUpgrade < inmortalLevels.Count - 1)
            actualInmortalUpgrade += 1;
    }
    public float GetInmortalDuration()
    {
        return inmortalLevels[actualInmortalUpgrade];
    }

    private int actualShotgunUpgrade = -1;
    public void UpgradeShotgun()
    {
        if (actualShotgunUpgrade < shotgun.stats.DamageLevels.Count - 1)
            actualShotgunUpgrade += 1;

        shotgun.stats.Damage = shotgun.stats.DamageLevels[actualShotgunUpgrade];
        shotgun.stats.MaxAmmo = shotgun.stats.AmmoLevels[actualShotgunUpgrade];
        shotgun.stats.ReloadTime = shotgun.stats.ReloadLevels[actualShotgunUpgrade];
    }

    private int actualRifleUpgrade = -1;
    public void UpgradeRifle()
    {
        if (actualRifleUpgrade < rifle.stats.DamageLevels.Count - 1)
            actualRifleUpgrade += 1;

        rifle.stats.Damage = rifle.stats.DamageLevels[actualRifleUpgrade];
        rifle.stats.MaxAmmo = rifle.stats.AmmoLevels[actualRifleUpgrade];
        rifle.stats.ReloadTime = rifle.stats.ReloadLevels[actualRifleUpgrade];
    }

    private int actualSubmachineUpgrade = -1;
    public void UpgradeSubmachine()
    {
        if (actualSubmachineUpgrade < submachine.stats.DamageLevels.Count - 1)
            actualSubmachineUpgrade += 1;

        submachine.stats.Damage = submachine.stats.DamageLevels[actualSubmachineUpgrade];
        submachine.stats.MaxAmmo = submachine.stats.AmmoLevels[actualSubmachineUpgrade];
        submachine.stats.ReloadTime = submachine.stats.ReloadLevels[actualSubmachineUpgrade];
    }

    private int actualPistolUpgrade = -1;
    public void UpgradePistol()
    {
        if (actualPistolUpgrade < pistol.stats.DamageLevels.Count - 1)
            actualPistolUpgrade += 1;

        pistol.stats.Damage = pistol.stats.DamageLevels[actualPistolUpgrade];
        pistol.stats.MaxAmmo = pistol.stats.AmmoLevels[actualPistolUpgrade];
        pistol.stats.ReloadTime = pistol.stats.ReloadLevels[actualPistolUpgrade];
    }

    private int actualSniperUpgrade = -1;
    public void UpgradeSniper()
    {
        if (actualSniperUpgrade < sniper.stats.DamageLevels.Count - 1)
            actualSniperUpgrade += 1;

        sniper.stats.Damage = sniper.stats.DamageLevels[actualSniperUpgrade];
        sniper.stats.MaxAmmo = sniper.stats.AmmoLevels[actualSniperUpgrade];
        sniper.stats.ReloadTime = sniper.stats.ReloadLevels[actualSniperUpgrade];
    }
}
