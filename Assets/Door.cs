using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    private Vector3 actorStartVector;
    public Vector3 actorDestinationVector;

    public Camera mainCamera;

    public Vector3 cameraOriginalVector;
    public Vector3 cameraDestinationVector;

    public GameObject player;

    bool isTransitioning;
    float transitionStartTime;
    public float transitionTime;

    public GameObject myLink;

    private Movement movementComponent;

    public void MoveCamera()
    {
        mainCamera.transform.position = cameraDestinationVector;
    }

    public void Interact(GameObject actor)
    {
        //myLink.SetActive(true);
        Component[] components = myLink.GetComponents(typeof(Component));
        foreach (Component component in components)
        {
            Debug.Log($"component: {component.ToString()}");
        }
        if (actor.name.ToLowerInvariant().Contains("player"))
        {
            PlayerUseDoor(actor);
        }
        else
        {
            //actor.transform.position = actorDestinationVector;
        }
    }

    public void PlayerUseDoor(GameObject player)
    {
        isTransitioning = true;
        transitionStartTime = Time.time;
        movementComponent = player.GetComponent<Movement>();
        movementComponent.TraverseNavMeshLink(actorDestinationVector);
    }

    private void Update()
    {
        if(isTransitioning)
        {
            float currentTransitionTime = Time.time - transitionStartTime;
            if (currentTransitionTime > transitionTime)
            {
                MoveCamera();
                isTransitioning = false;
                //myLink.SetActive(false); ;
            }
            else
            {
                mainCamera.transform.position = Vector3.Lerp(cameraOriginalVector, cameraDestinationVector, currentTransitionTime / transitionTime);
            }
        }



        if(Input.GetKeyDown(KeyCode.D))
        {
            Interact(player);
        }
    }
}
