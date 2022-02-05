using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryElementVisuals : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField]
    private Image hoverOutline;

    [SerializeField]
    private Image itemIcon;

 
    public void OnPointerDown(PointerEventData eventData)
    {
        itemIcon.color = new Color(0.5f, 0.5f, 0.5f, 1);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        itemIcon.color = new Color(1, 1, 1, 1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverOutline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverOutline.enabled = false;

        itemIcon.color = new Color(1, 1, 1, 1);
    }
}
