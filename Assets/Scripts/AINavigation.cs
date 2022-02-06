using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class AINavigation : MonoBehaviour
{
    private NavMeshAgent navAgent;

    [SerializeField]
    private AIScriptableObject AIScriptable;

    private float timeInShop = 0;


    bool isLookingAtNarcableObject = false;
    
    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = AIScriptable.walkSpeed;
    }

    private void Start()
    {
        float shouldWander = Random.Range(0, NavigationManager.instance.curiousityMax);
        ///Make it a little more likely that they might wander right away rather than going straight to the front desk
        if(shouldWander <= AIScriptable.curiousityLevel * 400)
        {
            Wander();
        }
        else
        {
            MoveToFrontDesk();
        }
        StartUpdateLoop();
    }

    private float timeLooking = 0;

    private float updateInteval = 1.0f;
    IEnumerator UpdateAtLongerFrequency()
    {
        yield return new WaitForSeconds(updateInteval);
        UpdateLoop();
        StartUpdateLoop();
        
    }

    private Color debugColor;
    private void Update()
    {
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 5), debugColor);
    }

    private void StartUpdateLoop()
    {
        StartCoroutine(UpdateAtLongerFrequency());
    }
    private void UpdateLoop()
    {
        timeInShop += updateInteval;

        switch (currentState)
        {
            case (AIStates.Looking):
                debugColor = Color.blue;
                timeLooking += updateInteval;
                if (isLookingAtNarcableObject)
                {

                    ///Chance to snitch
                    float shouldSnitch = Random.Range(0, NavigationManager.instance.narcMax);
                    if(shouldSnitch < AIScriptable.snitchLevel * timeLooking)
                    {
                        ///Snitch
                        Snitch();
                        break;
                    }
                }

                float shouldWander = Random.Range(0, NavigationManager.instance.curiousityMax);
                float shouldGoToFrontDesk = Random.Range(0, NavigationManager.instance.frontDeskWander);

                if(shouldWander <= AIScriptable.curiousityLevel * timeLooking)
                {
                    ///Wander
                    Wander();
                }
                else if (shouldGoToFrontDesk <= timeLooking)
                {
                    ///Go To Front Desk
                    MoveToFrontDesk();
                }
                break;
            case (AIStates.Wandering):
                debugColor = Color.green;
                if(Vector2.Distance(navAgent.transform.position,navAgent.destination) < 1f)
                {
                    currentState = AIStates.Looking;
                }
                break;
            case (AIStates.FrontDesk):
                debugColor = Color.red;
                timeLooking += updateInteval;
                shouldWander = Random.Range(0, NavigationManager.instance.curiousityMax);
                if(shouldWander <= AIScriptable.curiousityLevel * timeLooking)
                {
                    Wander();
                }
                break;
            case (AIStates.Exiting):
                debugColor = Color.cyan;
                break;
            case (AIStates.Snitching):
                debugColor = Color.black;
                break;
            case (AIStates.Curious):
                debugColor = Color.grey;
                break;
        }

        float shouldGoToExit = Random.Range(0, AIScriptable.maxTimeInShop);
        if (shouldGoToExit < timeInShop)
        {
            currentState = AIStates.Exiting;
            MoveToExit();
        }
    }


    private enum AIStates
    {
        Curious,
        Snitching,
        Exiting,
        Wandering,
        FrontDesk,
        Looking
    }

    private AIStates currentState;

    // Update is called once per frame

    private void Wander()
    {
        ///Find new point of interest to go to based on proximity
        timeLooking = 0;
        navAgent.SetDestination(NavigationManager.instance.GetPointOfInterest(transform.position,navAgent.destination));
        currentState = AIStates.Wandering;
    }

    private void Snitch()
    {
        timeLooking = 0;
        currentState = AIStates.Snitching;
        navAgent.speed = AIScriptable.runSpeed;
        MoveToExit();
    }

    private void MoveToFrontDesk()
    {
        currentState = AIStates.FrontDesk;
        timeLooking = 0;
        navAgent.SetDestination(NavigationManager.instance.frontDeskPosition);
    }

    private void MoveToExit()
    {
        timeLooking = 0;
        navAgent.SetDestination(NavigationManager.instance.exitPosition);
    }
}
