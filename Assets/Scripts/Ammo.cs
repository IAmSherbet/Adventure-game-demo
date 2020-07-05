using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    // Serialize an array where each item in array is of type AmmoSlot
    [SerializeField] AmmoSlot[] ammoSlots;

    // Make the private class AmmoSlot serializable
    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
    }

    public int GetCurrentAmmo(AmmoType currentWeaponAmmoType)
    {
        // Identify which ammoSlot holds the ammoType of the current weapon
        AmmoSlot currentAmmoSlot = GetAmmoSlot(currentWeaponAmmoType);

        // Return the ammoAmount remaining in the slot
        return currentAmmoSlot.ammoAmount;
    }

    public void ReduceAmmo(AmmoType currentWeaponAmmoType)
    {
        AmmoSlot reduceAmmoSlot = GetAmmoSlot(currentWeaponAmmoType);

        // Consume ammo in the slot
        if (reduceAmmoSlot.ammoAmount > 0)
        {
            reduceAmmoSlot.ammoAmount -= 1;
        }
    }

    public void IncreaseAmmo(int ammoGained, AmmoType ammoPickupType)
    {
        AmmoSlot increaseAmmoSlot = GetAmmoSlot(ammoPickupType);
        increaseAmmoSlot.ammoAmount += ammoGained;
    }

    private AmmoSlot GetAmmoSlot(AmmoType currentWeaponAmmoType)
    {
        // Loop through available slots to identify which ammoSlot holds the ammoType of current weapon
        foreach (AmmoSlot slot in ammoSlots)
        {
            if (slot.ammoType == currentWeaponAmmoType)
            {
                return slot;
            }
        }

        return null;
    }
}
