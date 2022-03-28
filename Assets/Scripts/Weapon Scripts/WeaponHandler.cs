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

    private float nextTimeToFire = 0f;

    private void Start()
    {
        MuzzleFlash = Instantiate(weapon.MuzzleFlash, muzzleFlashPoint);
    }

    private void Update()
    {
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

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, weapon.Range))
        {
            Debug.Log(hit.transform.name);

            EnemyHandler target = hit.transform.GetComponent<EnemyHandler>();
            if (target != null)
            {
                target.TakeDamage(weapon.Damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * weapon.ImpactForce);
            }

            if(impactEffect != null)
            {
                GameObject impactPrefab = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactPrefab, .5f);
            }
        }
    }

}
