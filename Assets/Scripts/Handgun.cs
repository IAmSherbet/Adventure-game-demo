using System;
using System.Collections;
using System.Collections.Generic;
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

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
            {
                Shoot();
            }
        }
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

            enemyHealth.TakeDamage(damage, hit);
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
