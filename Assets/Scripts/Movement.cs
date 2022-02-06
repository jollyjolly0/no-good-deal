#define DEBUG_BOUNDS_TRACKING

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{

    Rigidbody rb;

    [SerializeField]
    private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            if (horizontal != 0 || vertical != 0)
            {
                HandleInput(horizontal, vertical);
            }
    }

    private void HandleInput(float horizontal, float vertical)
    {
        rb.AddForce(new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime));
    }


}
