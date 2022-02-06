using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanInteractable : BaseInteractable
{
    public override string GetActionName()
    {
        return "Clean";
    }

    public override void Interact(GameObject actor)
    {
        Debug.Log("Cleaning");
    }

    public override bool InteractionCondition(GameObject actor)
    {
        var eqp = actor.transform.root.GetComponentInChildren<EquippedItem>();
        if (eqp == null) { return false; }

        if(  eqp.myHeldItem.GetType() == typeof(MopItem))
        {
            return true;
        }

        return false;

        
    }
}
