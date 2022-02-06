using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    [SerializeField]
    private float distanceModifier = 4;

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
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
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

    private class PointScore : IComparable<PointScore>
    {
        public float score;
        public Vector3 position;

        public PointScore(float score, Vector3 position)
        {
            this.score = score;
            this.position = position;
        }

        public int CompareTo(PointScore x)
        {
            return score.CompareTo(x.score);
        }
    }

    public Vector3 GetPointOfInterest(Vector3 position, Vector3 currentPointOfInterest)
    {
        int numOfPointsOfInterest = pointsOfInterest.Count;
        float maxScore = 0;
        List<PointScore> scores = new List<PointScore>();

        for(int i = 0; i < pointsOfInterest.Count; i++)
        {
            if(currentPointOfInterest != null && pointsOfInterest[i].x == currentPointOfInterest.x && pointsOfInterest[i].z == currentPointOfInterest.z)
            {
                continue;
            }
            float myScore = 1.0f/Vector3.Distance(position, pointsOfInterest[i]);
            if (myScore <= 0.5f)
            {

                continue;
            }
            scores.Add(new PointScore(myScore,pointsOfInterest[i]));
            maxScore += myScore;
        }
        scores.Sort();
        float randomPercent = UnityEngine.Random.Range(0.0f, 1.0f);
        float currentScoreCount = 0;
        foreach(PointScore score in scores)
        {
            currentScoreCount += score.score / maxScore;
            if(randomPercent <= currentScoreCount)
            {
                return score.position;
            }
        }

        ///This should never happen now
        return pointsOfInterest[UnityEngine.Random.Range(0, numOfPointsOfInterest)];
    }
}
