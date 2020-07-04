using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] float range = 10f;
    [SerializeField] float damage = 25f;
    private float timeBetweenShots = 0.1f;

    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Transform parent;
    [SerializeField] Ammo ammoSlot;

    private int counter = 0;

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (ammoSlot.GetCurrentAmmo() > 0)
            {
                StartCoroutine("MachineGunShoot", timeBetweenShots);
            }
        }
    }

    IEnumerator MachineGunShoot(float timeBetweenShots)
    {
        yield return new WaitForSeconds(timeBetweenShots);

        counter++;
        print("Shots fired: " + counter);

        PlayMuzzleFlash();
        ProcessRaycast();
        ammoSlot.ReduceAmmo();

        StopCoroutine("MachineGunShoot");

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
