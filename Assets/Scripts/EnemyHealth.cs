using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints = 0f;

    public void GainEnergy(float energy)
    {
        hitPoints += energy;
        print(hitPoints);
        //todo: do something when hitPoints reaches > 100
    }
}
