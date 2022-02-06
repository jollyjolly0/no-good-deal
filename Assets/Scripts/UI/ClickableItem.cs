using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ClickableItem : MonoBehaviour , IPointerClickHandler
{
    IItemClickHandler handler;
    private BaseItem itemFunctionality;

    private void Awake()
    {
        itemFunctionality = GetComponent<BaseItem>();
    }

    private void Start()
    {
        handler = GetComponentInParent<IItemClickHandler>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        handler.HandleItemUse(itemFunctionality);
        //itemFunctionality.Use();
    }


}
