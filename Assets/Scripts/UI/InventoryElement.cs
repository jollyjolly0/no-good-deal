using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventoryElement : MonoBehaviour , IPointerClickHandler
{

    private BaseItem itemFunctionality;

    private void Awake()
    {
        itemFunctionality = GetComponent<BaseItem>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        itemFunctionality.Use();
    }


}
