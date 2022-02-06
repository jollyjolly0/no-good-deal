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
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            itemFunctionality.Use();
        }

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            ItemDescriptionMenu.instance.OpenMenu(itemFunctionality.itemScriptableObject.itemName, itemFunctionality.itemScriptableObject.itemDescription, itemFunctionality.itemScriptableObject.itemImage);
        }
    }


}
