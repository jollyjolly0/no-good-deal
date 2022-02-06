using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    [HideInInspector]
    public List<Vector3> pointsOfInterest;

    [HideInInspector]
    public Vector3 frontDeskPosition;

    [HideInInspector]
    public Vector3 exitPosition;

    public static NavigationManager instance;

    public float curiousityMax = 100.0f;
    public float narcMax = 100.0f;
    public float frontDeskWander = 100.0f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        GameObject[] destinationPoints = GameObject.FindGameObjectsWithTag("DestinationPoint");
        foreach(GameObject point in destinationPoints)
        {
            pointsOfInterest.Add(point.transform.position);
        }

        frontDeskPosition = GameObject.FindGameObjectWithTag("FrontDesk").transform.position;
        exitPosition = GameObject.FindGameObjectWithTag("Exit").transform.position;
    }

    public Vector3 GetPointOfInterest()
    {
        int numOfPointsOfInterest = pointsOfInterest.Count;
        return pointsOfInterest[Random.Range(0, numOfPointsOfInterest)];
    }
}
