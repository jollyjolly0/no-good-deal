using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


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
        currentState = AIStates.Returning;
        StartUpdateLoop();
        StartCoroutine(TestQuesting());
    }

    private float timeLooking = 0;

    private float timeQuesting = 0;

    private float questTime = 0;

    private float waitingToReturnTime = 0;

    private float updateInteval = 1.0f;
    IEnumerator UpdateAtLongerFrequency()
    {
        UpdateLoop(UnityEngine.Random.Range(0,1000));
        yield return new WaitForSeconds(updateInteval);
        StartUpdateLoop();
        
    }

    private Color debugColor;

    IEnumerator TestQuesting()
    {
        yield return new WaitForSeconds(5.0f);
        SetQuestTime(30);
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), debugColor);
    }

    private void StartUpdateLoop()
    {
        StartCoroutine(UpdateAtLongerFrequency());
    }
    private void UpdateLoop(int updateNum)
    {
        if(currentState != AIStates.Questing && currentState != AIStates.WaitingToReturn)
        {
            timeInShop += updateInteval;
        }

        if (currentState != AIStates.Exiting || currentState != AIStates.Snitching || currentState != AIStates.Questing)
        {
            float shouldGoToExit = Random.Range(0, AIScriptable.maxTimeInShop);
            if (shouldGoToExit < timeInShop)
            {
                currentState = AIStates.Exiting;
                MoveToExit();
            }
        }


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
                if(Vector2.Distance(navAgent.transform.position, NavigationManager.instance.exitPosition) < 1f)
                {
                    currentState = AIStates.WaitingToReturn;
                }
                debugColor = Color.cyan;
                break;
            case (AIStates.Snitching):
                debugColor = Color.black;
                break;
            case (AIStates.Curious):
                debugColor = Color.grey;
                break;
            case (AIStates.WaitingToReturn):
                waitingToReturnTime += updateInteval;
                float shouldReturn = Random.Range(0, AIScriptable.maxTimeWaitingToReturn);
                if(shouldReturn > waitingToReturnTime)
                {
                    currentState = AIStates.Returning;
                }
                break;
            case (AIStates.Returning):
                shouldWander = Random.Range(0, NavigationManager.instance.curiousityMax);
                ///Make it a little more likely that they might wander right away rather than going straight to the front desk
                if (shouldWander <= AIScriptable.curiousityLevel * 400)
                {
                    Wander();
                }
                else
                {
                    MoveToFrontDesk();
                }
                break;
            case (AIStates.Questing):
                debugColor = Color.white;
                timeQuesting += updateInteval;
                if(timeQuesting >= questTime)
                {
                    currentState = AIStates.Returning;
                }
                break;
        }

    }


    private enum AIStates
    {
        Curious,
        Snitching,
        Exiting,
        Wandering,
        FrontDesk,
        Looking,
        WaitingToReturn,
        Returning,
        Questing
    }

    private AIStates currentState;

    PointOfInterest currentPoi;
    // Update is called once per frame

    private void Wander()
    {
        if(currentState == AIStates.Exiting)
        {
            return;
        }
        ///Find new point of interest to go to based on proximity
        timeLooking = 0;
        currentPoi = NavigationManager.instance.GetPointOfInterest(transform.position, currentPoi);
        navAgent.SetDestination(currentPoi.transform.position);
        currentState = AIStates.Wandering;
    }

    private void Snitch()
    {
        if(currentState == AIStates.Exiting)
        {
            return;
        }
        timeLooking = 0;
        currentState = AIStates.Snitching;
        navAgent.speed = AIScriptable.runSpeed;
        MoveToExit();
    }

    private void MoveToFrontDesk()
    {
        if(currentState == AIStates.Exiting)
        {
            return;
        }
        currentState = AIStates.FrontDesk;
        timeLooking = 0;
        navAgent.SetDestination(NavigationManager.instance.frontDeskPosition);
    }

    private void MoveToExit()
    {
        timeLooking = 0;
        waitingToReturnTime = 0;
        timeInShop = 0;
        navAgent.SetDestination(NavigationManager.instance.exitPosition);
    }

    public void SetQuestTime(float time)
    {
        timeQuesting = 0;
        questTime = time;
        MoveToExit();
        currentState = AIStates.Questing;
    }
}
