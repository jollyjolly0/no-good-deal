using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    [HideInInspector]
    public List<Vector3> pointsOfInterest;

    public static NavigationManager instance;

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
    }

    public Vector3 GetPointOfInterest()
    {
        int numOfPointsOfInterest = pointsOfInterest.Count;
        return pointsOfInterest[Random.Range(0, numOfPointsOfInterest)];
    }
}
