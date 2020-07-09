using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100f;

    public void TakeDamage(float damage, RaycastHit hit)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        print(hitPoints);

        if (hitPoints <= Mathf.Epsilon)
        {
            //Destroy(gameObject);
            GetComponent<EnemyDeath>().ActivateRagdoll(hit);
        }
    }
}
