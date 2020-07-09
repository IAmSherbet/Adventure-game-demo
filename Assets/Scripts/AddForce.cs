using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] float thrust = 50f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(thrust * Vector3.right);
    }

}
