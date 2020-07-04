using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;

    void Start()
    {
        SetActiveWeapon();
    }

    private void Update()
    {
        // set the previous weapon as the current weapon
        int previousWeapon = currentWeapon;

        // check for input to change current weapon int
        ProcessKeyInput();

        // if the current weapon int has been changed, set the active weapon
        if (previousWeapon != currentWeapon)
        {
            SetActiveWeapon();
        }
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
    }

    void SetActiveWeapon()
    {
        int weaponIndex = 0;

        foreach (Transform weapon in transform) {
            if (weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            weaponIndex++;
        }
    }
}
