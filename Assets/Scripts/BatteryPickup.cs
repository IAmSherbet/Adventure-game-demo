using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    Flashlight flashlight;

    private void Start()
    {
        flashlight = FindObjectOfType<Flashlight>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                break;
            case "Player":
                print("battery collision with player");
                flashlight.AddStoredBattery();
                Destroy(gameObject);
                break;
            default:
                break;
        }

    }
}
