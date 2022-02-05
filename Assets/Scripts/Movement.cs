using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    private float deadInputLimit = 0.1f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal > deadInputLimit || horizontal < -deadInputLimit || vertical > deadInputLimit || vertical < -deadInputLimit)
        {
            HandleInput(horizontal, vertical);
        }
    }

    private void HandleInput(float horizontal, float vertical)
    {
        Vector3 currentPos = transform.position;
        Vector3 newPos = currentPos;
        newPos.x = newPos.x + (horizontal * 10);
        newPos.z = newPos.z + (vertical * 10);
        navMeshAgent.SetDestination(newPos);
    }


}
