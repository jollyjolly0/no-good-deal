using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{

    public virtual void Use()
    {
        Debug.Log("using "+ name);
    }

}
