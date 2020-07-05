using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 10;
    [SerializeField] AmmoType ammoType;

    private void OnTriggerEnter(Collider other)
    {
        Ammo ammoSlot = FindObjectOfType<Ammo>();

        switch (other.gameObject.tag)
        {
            case "Enemy":
                break;
            case "Player":
                print("Collision with player");
                ammoSlot.IncreaseAmmo(ammoAmount, ammoType);
                //Destroy pick up
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
