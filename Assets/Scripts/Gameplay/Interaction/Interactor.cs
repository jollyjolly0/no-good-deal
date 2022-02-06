using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    private StringEvent contextChanged;

    private List<BaseInteractable> currentInteractables;
    private BaseInteractable cachedCurrent = null;
    [SerializeField]
    private HeldItemEvent changedHeldItem;

    private void Awake()
    {
        currentInteractables = new List<BaseInteractable>();
        changedHeldItem.Fired += ChangedHeldItem_Fired;
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var current = GetCurrentInteract();
            if (current == null)
            {
                Debug.Log("nothign there");
            }
            else
            {
                current.Interact(this.gameObject);
            }
        }
    }

    private void ChangedHeldItem_Fired(object sender, HeldItem e)
    {
        CheckContextChanged();
    }


    private BaseInteractable GetCurrentInteract()
    {
            BaseInteractable closest = null;
            float closestDistance = Mathf.Infinity;

            foreach (var item in currentInteractables)
            {
                if (item.InteractionCondition(this.gameObject.transform.root.gameObject) == false) { break; }

                float dist = Vector3.Distance(transform.position, item.GetPosition());
                if (dist < closestDistance)
                {
                closest = item;
                closestDistance = dist;
                }
            }

        return closest;
    }

    private void CheckContextChanged()
    {
        var cur = GetCurrentInteract();

        if(cur != cachedCurrent)
        {
            contextChanged.Invoke(this, cur != null? cur.GetActionName() : "___");
            cachedCurrent = cur;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        var inter =collision.GetComponentInParent<BaseInteractable>();
        if(inter != null)
        {
            currentInteractables.Add(inter);
            CheckContextChanged();
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var inter = collision.GetComponentInParent<BaseInteractable>();
        if (inter != null && currentInteractables.Contains(inter))
        {
            currentInteractables.Remove(inter);
            CheckContextChanged();
        }
    }
}
