using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] float dyingBlowForce = 1000f;

    public Rigidbody rigidBody;
    public EnemyAI enemyAI;
    public NavMeshAgent navMeshAgent;
    public List<Collider> ragdollColliders = new List<Collider>();

    private void Awake()
    {
        SetRagdollParts();
    }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        enemyAI = GetComponent<EnemyAI>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void SetRagdollParts()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
        {
            if (c.gameObject != gameObject)
            {
                c.enabled = false;
                ragdollColliders.Add(c);
            }
        }
    }

    public void ActivateRagdoll(RaycastHit hit)
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.useGravity = false;

        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<Animator>().avatar = null;

        foreach (Collider c in ragdollColliders)
        {
            if (c.gameObject != gameObject)
            {
                c.enabled = true;
                c.attachedRigidbody.velocity = Vector3.zero;
                c.attachedRigidbody.AddExplosionForce(dyingBlowForce, hit.point, -1f);
            }
        }

        //Destroy(rigidBody);

        enemyAI.enabled = false;
        navMeshAgent.enabled = false;
    }
}
