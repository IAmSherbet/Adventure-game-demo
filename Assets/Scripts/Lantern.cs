using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] float range = 10f;
    [SerializeField] float energy = 25f;
    [SerializeField] ParticleSystem lanternFlash;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        PlayLanternFlash();
        ProcessRaycast();
    }

    private void PlayLanternFlash()
    {
        lanternFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        var rayCast = Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range);
        if (rayCast)
        {
            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth == null) { return; }
            enemyHealth.GainEnergy(energy);
        }
        else
        {
            return;
        }
    }
}
