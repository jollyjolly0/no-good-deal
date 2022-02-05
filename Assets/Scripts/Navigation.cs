using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Navigation : MonoBehaviour
{
    private NavMeshAgent navAgent;
    [SerializeField]
    private LayerMask collisionMask;
    [SerializeField]
    private float minWaitTimeToMove = 2.0f;
    [SerializeField]
    private float maxWaitTimeToMove = 5.0f;
    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        RandomCharacterMovement(0.0f);
    }

    // Update is called once per frame


    IEnumerator WaitAndThenMovePosition(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        navAgent.destination = NavigationManager.instance.GetPointOfInterest();
        RandomCharacterMovement(Random.Range(minWaitTimeToMove, maxWaitTimeToMove));
    }

    private void RandomCharacterMovement(float waitTime)
    {
        StartCoroutine(WaitAndThenMovePosition(waitTime));
    }
}
