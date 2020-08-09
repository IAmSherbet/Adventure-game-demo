﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Handgun : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] float range = 50f;
    [SerializeField] float damage = 17f;

    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Transform parent;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;

    [SerializeField] TextMeshProUGUI ammoText;

    void Update()
    {
        DisplayAmmo();

        if (Input.GetButtonDown("Fire1"))
        {
            if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
            {
                Shoot();
            }
        }
    }

    private void DisplayAmmo()
    {
        var currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = "Handgun: " + currentAmmo.ToString();
    }

    private void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRaycast();
        ammoSlot.ReduceAmmo(ammoType);
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlashVFX.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        var rayCast = Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range);
        if (rayCast)
        {
            CreateHitEffect(hit);

            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();

            if (enemyHealth == null) { return; }

            enemyHealth.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    private void CreateHitEffect(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal), parent);
        Destroy(impact, 1);
    }
}
