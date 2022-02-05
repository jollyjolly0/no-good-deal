using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractable :MonoBehaviour
{
    public virtual void Interact() { Debug.LogWarning("calling an unimplemented Interactable"); }
    public virtual string GetActionName() { Debug.LogWarning("calling an unimplemented Interactable"); return null; }
    public virtual Vector3 GetPosition() { return transform.position; }
    public virtual int GetPriority() { return 0; }


}

