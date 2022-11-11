using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public WeaponStats stats;

    [SerializeField] private Camera fpsCam;
    [SerializeField] private GameObject impactEffect;

    private ParticleSystem MuzzleFlash;
    [SerializeField] private Transform muzzleFlashPoint;

    [SerializeField] private Animator anim;

    private float nextTimeToFire = 0f;
    private int currentAmmo;
    private bool isReloading = false;

    public LayerMask rayCollision;

    private PlayerHandler player;

    private void Start()
    {
        player = GetComponentInParent<PlayerHandler>();
        MuzzleFlash = Instantiate(stats.MuzzleFlash, muzzleFlashPoint);
        currentAmmo = stats.MaxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
        anim.SetBool("Reloading",false);
    }

    private void Update()
    {
        if (isReloading)
            return;

        if (player.dontReload)
            return;

        if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R) && currentAmmo < stats.MaxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        UIManager.instance.UpdateAmmo(currentAmmo, stats.MaxAmmo);
    }

    private void FixedUpdate()
    {
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

        if(!player.dontReload)
            currentAmmo--;

        //Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, stats.Range, rayCollision))
        {
            EnemyHandler target = hit.transform.GetComponent<EnemyHandler>();
            if (target != null)
            {
                target.TakeDamage(stats.Damage);
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
        Debug.Log("Reloading...");
        UIManager.instance.Reloading();
        anim.SetBool("Reloading", true);
        yield return new WaitForSeconds(stats.ReloadTime - .25f);
        anim.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = stats.MaxAmmo;
        isReloading = false;
    }

}
