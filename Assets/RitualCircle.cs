using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualCircle : MonoBehaviour
{
    public List<BaseInteractable> ritualBowls;

    private void Update()
    {        
        if(CheckComplete())
        {
            Debug.Log("RitualComplete");
            Debug.Log("RitualComplete");
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
