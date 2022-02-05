using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanInteractable : BaseInteractable
{
    public override string GetActionName()
    {
        return "Clean";
    }

    public override void Interact()
    {
        Debug.Log("Cleaning");
    }
}
