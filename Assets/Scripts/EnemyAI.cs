using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float dyingBlowForce = 1000f;

    Transform targetToChase;

    public List<Collider> ragdollParts = new List<Collider>();
    public Rigidbody rigidBody;
    public float speed;
    public float angularSpeed;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    private void Awake()
    {
        SetRagdollParts();
    }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        targetToChase = FindObjectOfType<PlayerHealth>().gameObject.transform;
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    private void FixedUpdate()
    {
        speed = rigidBody.velocity.magnitude;
        angularSpeed = rigidBody.angularVelocity.magnitude;
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(targetToChase.position, transform.position);

        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
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

    public void ActivateRagdoll()
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
                c.attachedRigidbody.AddForce(Vector3.back * dyingBlowForce);
            }
        }

        gameObject.GetComponent<EnemyAI>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    void EngageTarget()
    {
        FaceTarget();

        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(targetToChase.position);
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (targetToChase.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
