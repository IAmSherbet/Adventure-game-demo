using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private int ammoAmount = 200;

    public int GetCurrentAmmo()
    {
        return ammoAmount;
    }

    public void ReduceAmmo()
    {
        if (ammoAmount > 0)
        {
            ammoAmount -= 1;
        }
    }
}
