using System;
using System.Collections;
using System.Collections.Generic;
using TooManyCrosshairs;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public WeaponStats stats;
    public LayerMask rayCollision;
    [Header("VFX Settings")]
    [SerializeField] private Camera fpsCam;
    [SerializeField] private GameObject impactEffect;

    private ParticleSystem MuzzleFlash;
    [SerializeField] private Transform muzzleFlashPoint;
    [Header("SFX Settings")]
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource shootAudioSource;
    [SerializeField] private AudioSource reloadAudioSource;
    [Header("Cross Hair Settings")]
    [SerializeField] private GameObject crossHairContainer;
    [SerializeField] private Crosshair crossHair;
    [SerializeField] private float gunRecoil;
    [SerializeField] private float settleSpeed;
    [SerializeField] private float maxCrossHairSize;
    [SerializeField] private Color specialColor;


    private float nextTimeToFire = 0f;
    private int currentAmmo;
    private bool isReloading = false;
    private bool isRunning = false;


    private PlayerHandler player;

    private void Start()
    {
        player = GetComponentInParent<PlayerHandler>();
        MuzzleFlash = Instantiate(stats.MuzzleFlash, muzzleFlashPoint);
        currentAmmo = stats.MaxAmmo;
        crossHair.SetShrinkSpeed(settleSpeed);
        crossHair.SetMaxScale(maxCrossHairSize);
    }

    private void OnEnable()
    {
        crossHairContainer.SetActive(true);

        isReloading = false;
        anim.SetBool("Reloading",false);
        if (stats.EquipSFX != null)
        {
            reloadAudioSource.volume = stats.EquipVolume;
            reloadAudioSource.PlayOneShot(stats.EquipSFX);
        }
    }

    private void OnDisable()
    {
        crossHairContainer?.SetActive(false);
    }

    private void Update()
    {
        if (isRunning)
            return;

        if (isReloading)
            return;

        if (player.dontReload)
            return;

        if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R) && currentAmmo < stats.MaxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
            MaxUpgrades();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Running", true);
            anim.SetBool("Reloading", false);
            isReloading = false;
            isRunning = true;
            crossHair.CancelReload();
            StopAllCoroutines();
        }
        else
        {
            anim.SetBool("Running", false);
            isRunning = false;
        }

        if (isRunning)
            return;

        if (isReloading)
            return;

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / stats.FireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        if(stats.MuzzleFlash != null)
            MuzzleFlash.Play();

        if (stats.ShootsSFX.Length > 0)
        {
            shootAudioSource.volume = stats.ShootVolume;
            shootAudioSource.PlayOneShot(stats.ShootsSFX[UnityEngine.Random.Range((int)0, (int)stats.ShootsSFX.Length)]);
        }

        if(!player.dontReload)
            currentAmmo--;

        UIManager.instance.UpdateAmmo(currentAmmo, stats.MaxAmmo);

        crossHair.ExpandCrosshair(gunRecoil);

        //Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, stats.Range, rayCollision))
        {
            EnemyHandler target = hit.transform.GetComponent<EnemyHandler>();
            if (target != null)
            {
                target.TakeDamage(stats.Damage);
                ScoreManager.instance.AddScore((int)(stats.Damage * .10f));
            }

            if(impactEffect != null)
            {
                GameObject impactPrefab = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactPrefab, .5f);
            }
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        crossHair.SetReloadSpeed(1f / stats.ReloadTime);
        crossHair.DoReload();

        anim.SetBool("Reloading", true);
        yield return new WaitForSeconds(stats.ReloadTime - .25f);
        anim.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);
        if (stats.ReloadsSFX.Length > 0)
        {
            reloadAudioSource.volume = stats.ReloadVolume;
            reloadAudioSource.PlayOneShot(stats.ReloadsSFX[UnityEngine.Random.Range((int)0, (int)stats.ReloadsSFX.Length)]);
        }
        currentAmmo = stats.MaxAmmo;
        isReloading = false;
        UIManager.instance.UpdateAmmo(currentAmmo, stats.MaxAmmo);
    }

    public void MaxUpgrades()
    {
        ShowAlts();
        EnableTint();
    }

    void ShowAlts() // tells the crosshair to show the alternate textures
    {
        crossHair.ShowAlternates();
    }

    void HideAlts() // tells the crosshair to show the default textures
    {
        crossHair.HideAlternates();
    }

    void EnableTint() // tells the crosshair to show the alternate color
    {
        crossHair.EnableTint(specialColor);
    }

    void DisableTint() // tells the crosshair to show the default color
    {
        crossHair.DisableTint();
    }

}
