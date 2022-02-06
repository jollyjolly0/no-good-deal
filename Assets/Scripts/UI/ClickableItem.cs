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
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            handler.HandleItemUse(itemFunctionality);
        }

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            ItemDescriptionMenu.instance.OpenMenu(itemFunctionality.itemScriptableObject.itemName, itemFunctionality.itemScriptableObject.itemDescription, itemFunctionality.itemScriptableObject.itemImage);
        }
    }


}
