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
                timeLooking += Time.deltaTime;
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
                if(Vector2.Distance(navAgent.transform.position,navAgent.destination) < 0.01f)
                {
                    currentState = AIStates.Looking;
                }
                break;
            case (AIStates.FrontDesk):
                timeLooking += updateInteval;
                shouldWander = Random.Range(0, NavigationManager.instance.curiousityMax);
                if(shouldWander <= AIScriptable.curiousityLevel * timeLooking)
                {
                    Wander();
                }
                break;
            case (AIStates.Exiting):
                break;
            case (AIStates.Snitching):
                break;
            case (AIStates.Curious):
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
        currentState = AIStates.Wandering;
        timeLooking = 0;
        navAgent.SetDestination(NavigationManager.instance.GetPointOfInterest());
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
