using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    private int selectedWeapon = 0;
    private int currentWeapon;

    private void Update()
    {
        currentWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) 
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) 
        {
            if (selectedWeapon <= 0) 
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (currentWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

    }
    private void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
