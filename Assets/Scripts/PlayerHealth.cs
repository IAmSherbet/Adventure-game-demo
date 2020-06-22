using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    public void TakeDamage(float damage)
    {
        if (hitPoints <= Mathf.Epsilon) { return; }
        hitPoints -= damage;
        print("Player health = " + hitPoints);
    }
}
