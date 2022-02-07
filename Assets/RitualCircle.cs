using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualCircle : MonoBehaviour
{
    public List<BaseInteractable> ritualBowls;
    public SpriteRenderer portal;

    private void Update()
    {        
        if(CheckComplete())
        {
            Debug.Log("RitualComplete");
            portal.enabled = true;
        }
    }

    public bool CheckComplete()
    {
        bool complete = true;
        foreach (RitualBowlInteractable ritualBowl in ritualBowls)
        {
            if (!ritualBowl.filled)
                complete = false;
        }
        return complete;
    }
}
