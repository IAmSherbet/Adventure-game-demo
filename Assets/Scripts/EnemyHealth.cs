using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100f;

    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;

        if (hitPoints <= Mathf.Epsilon)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        if (isDead) { return; }
        isDead = true;
        GetComponent<Animator>().SetTrigger("dead");
    }
}
