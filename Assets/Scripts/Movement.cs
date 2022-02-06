#define DEBUG_BOUNDS_TRACKING

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    [SerializeField]
    private LayerMask obstacleLayerMask;

#if DEBUG_BOUNDS_TRACKING
    private float oldHorizontal;
    private float oldVertical;
#endif

    [SerializeField]
    private Vector3 xHalfExtents = new Vector3(0.05f,0.01f,0.5f);
    [SerializeField]
    private Vector3 zHalfExtents = new Vector3(0.5f,0.01f,0.05f);

    //This is the yPos of the sprite that is rendered;
    [SerializeField]
    private float yPos = 0.1f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (navMeshAgent.isOnNavMesh)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            if (horizontal != 0 || vertical != 0)
            {
                HandleInput(horizontal, vertical);
                oldHorizontal = horizontal;
                oldVertical = vertical;
            }
            else
            {
                StopMovement();
            }
        }

#if DEBUG_BOUNDS_TRACKING
        DrawLastKnownBounds(oldHorizontal, oldVertical);
#endif
    }

    private void StopMovement()
    {
        navMeshAgent.SetDestination(transform.position);
    }

    private void HandleInput(float horizontal, float vertical)
    {
        Vector3 currentPos = transform.position;

        ///This is where we want to move to
        Vector3 newPos = currentPos;
        newPos.x = newPos.x + (horizontal/10);
        newPos.z = newPos.z + (vertical/10);

        ///These will be our center points(x and z direction) for our bounding boxes that we use to check for collisions with obstacles
        Vector3[] centerPoints = GetBoundsCenterPoints(horizontal,vertical);
        
        ///If we collide with an obstacle, then move back to the last known safe space in that direction
        if (Physics.CheckBox(centerPoints[0], xHalfExtents, Quaternion.identity,obstacleLayerMask))
        {
            newPos.x = currentPos.x;
        }
        if (Physics.CheckBox(centerPoints[1], zHalfExtents, Quaternion.identity,obstacleLayerMask))
        {
            newPos.z = currentPos.z;
        }
        navMeshAgent.SetDestination(newPos);
    }


    /// <summary>
    /// Purely for debugging, this draws our bounding box for collision detection with obstacles
    /// </summary>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    private void DrawLastKnownBounds(float horizontal, float vertical)
    {
        Vector3[] centerPoints = GetBoundsCenterPoints(horizontal, vertical);

        Vector3 startPointX = centerPoints[0];
        startPointX.x -= xHalfExtents.x;
        startPointX.z -= xHalfExtents.z;
        Vector3 startPointX2 = startPointX;
        startPointX2.x += (2 * xHalfExtents.x);

        Vector3 startPointZ = centerPoints[1];
        startPointZ.z -= zHalfExtents.z;
        startPointZ.x -= zHalfExtents.x;
        Vector3 startPointZ2 = startPointZ;
        startPointZ2.z += (2 * zHalfExtents.z);



        Debug.DrawLine(startPointX, new Vector3(startPointX.x, startPointX.y,startPointX.z + (2 * xHalfExtents.z)));
        Debug.DrawLine(startPointX2, new Vector3(startPointX2.x, startPointX2.y, startPointX2.z + (2 * xHalfExtents.z)));

        Debug.DrawLine(startPointZ, new Vector3(startPointZ.x + (2 * zHalfExtents.x), startPointZ.y, startPointZ.z));
        Debug.DrawLine(startPointZ2, new Vector3(startPointZ2.x + (2 * zHalfExtents.x), startPointZ2.y, startPointZ2.z));
    }

    /// <summary>
    /// Returns the horizontal and vertical center points for the bounds boxes used to detect obstacles
    /// </summary>
    /// <returns></returns>
    private Vector3[] GetBoundsCenterPoints(float horizontal, float vertical)
    {
        Vector3[] centerPoints = new Vector3[2];

        Vector3 currentPos = transform.position;

        Vector3 horizontalBoxCenter = currentPos;
        Vector3 verticalBoxCenter = currentPos;
        horizontalBoxCenter.y += 0.1f;
        verticalBoxCenter.y += 0.1f;
        horizontalBoxCenter.x += (horizontal / 2) + (horizontal * xHalfExtents.x);
        verticalBoxCenter.z += (vertical / 2) + (vertical * zHalfExtents.z);

        centerPoints[0] = horizontalBoxCenter;
        centerPoints[1] = verticalBoxCenter;

        return centerPoints;
    }

    public void TraverseNavMeshLink(Vector3 endPos)
    {
        navMeshAgent.SetDestination(endPos);
        navMeshAgent.Warp(endPos);
        navMeshAgent.ActivateCurrentOffMeshLink(false);
    }


}
