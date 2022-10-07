using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private WeaponStats weapon;

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
        MuzzleFlash = Instantiate(weapon.MuzzleFlash, muzzleFlashPoint);
        currentAmmo = weapon.MaxAmmo;
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

        if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R) && currentAmmo < weapon.MaxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        UIManager.instance.UpdateAmmo(currentAmmo, weapon.MaxAmmo);
    }

    private void FixedUpdate()
    {
        if (isReloading)
            return;

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / weapon.FireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        if(weapon.MuzzleFlash != null)
            MuzzleFlash.Play();

        if(!player.dontReload)
            currentAmmo--;

        //Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, weapon.Range, rayCollision))
        {
            //Debug.Log(hit.transform.name);

            EnemyHandler target = hit.transform.GetComponent<EnemyHandler>();
            if (target != null)
            {
                target.TakeDamage(weapon.Damage);
            }

            //if(hit.rigidbody != null)
            //{
            //    hit.rigidbody.AddForce(-hit.normal * weapon.ImpactForce);
            //}

            if(impactEffect != null)
            {
                GameObject impactPrefab = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactPrefab, .5f);
            }
        }



        //RaycastHit[] hits;

        //hits = new RaycastHit[10];

        //for(int i = 0; i < hits.Length; i++)
        //{

        //    Vector3 spreaadDirection = fpsCam.transform.forward + new Vector3(Random.RandomRange(-1f, 1f), Random.RandomRange(-1f, 1f), 0);


        //    if(Physics.Raycast(fpsCam.transform.position, spreaadDirection, out hits[i], weapon.Range))
        //    {

        //    }
        //}
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        UIManager.instance.Reloading();
        anim.SetBool("Reloading", true);
        yield return new WaitForSeconds(weapon.ReloadTime - .25f);
        anim.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = weapon.MaxAmmo;
        isReloading = false;
    }

}
