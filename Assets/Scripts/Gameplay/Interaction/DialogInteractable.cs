using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInteractable : BaseInteractable
{
    public override string GetActionName()
    {
        return "Talk";
    }

    public override void Interact()
    {
        Debug.Log("Spoke when speaken to");
    }


}
