﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 25f;
    private float timeBetweenShots = 0.1f;

    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Transform parent;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;

    [SerializeField] TextMeshProUGUI ammoText;

    void Update()
    {
        DisplayAmmo();

        if (Input.GetButton("Fire1"))
        {
            if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
            {
                StartCoroutine("Shoot", timeBetweenShots);
            }
        }
    }

    private void DisplayAmmo()
    {
        var currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = "Rifle: " + currentAmmo.ToString();
    }



    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(timeBetweenShots);

        PlayMuzzleFlash();
        ProcessRaycast();
        ammoSlot.ReduceAmmo(ammoType);

        StopCoroutine("Shoot");

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
