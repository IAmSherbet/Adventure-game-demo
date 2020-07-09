using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] float dyingBlowForce = 1000f;

    public Rigidbody rigidBody;
    public EnemyAI enemyAI;
    public NavMeshAgent navMeshAgent;
    public List<Collider> ragdollParts = new List<Collider>();

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

    private void Update()
    {
        
    }

    private void SetRagdollParts()
    {
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
        {
            if (c.gameObject != gameObject)
            {
                c.isTrigger = true;
                ragdollParts.Add(c);
            }
        }
    }

    public void ActivateRagdoll(RaycastHit hit)
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.useGravity = false;

        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<Animator>().avatar = null;

        foreach (Collider c in ragdollParts)
        {
            if (c.gameObject != gameObject)
            {
                c.isTrigger = false;
                c.attachedRigidbody.velocity = Vector3.zero;
                c.attachedRigidbody.angularVelocity = Vector3.zero;
                c.attachedRigidbody.AddForce(hit.normal * dyingBlowForce * -1);
            }
        }

        Destroy(rigidBody);

        enemyAI.enabled = false;
        navMeshAgent.enabled = false;
    }
}
