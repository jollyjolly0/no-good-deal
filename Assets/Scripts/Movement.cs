using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    private float deadInputLimit = 0.5f;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            HandleInput(horizontal, vertical);
        }
        else
        {
            StopMovement();
        }
    }

    private void StopMovement()
    {
        navMeshAgent.SetDestination(transform.position);
    }

    private void HandleInput(float horizontal, float vertical)
    {
        Vector3 currentPos = transform.position;
        Vector3 newPos = currentPos;
        newPos.x = newPos.x + (horizontal);
        newPos.z = newPos.z + (vertical);
        navMeshAgent.SetDestination(newPos);
    }


}
