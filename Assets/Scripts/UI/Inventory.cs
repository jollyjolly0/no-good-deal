using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Inventory : MonoBehaviour,
    IItemClickHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    const int inventoryWidth = 4;
    const int inventoryHeight = 7;
    const int inventorySize = inventoryWidth * inventoryHeight;
    BaseItem[] items;


    HeldItem currentHeldItem = null;
    [SerializeField]
    private HeldItemEvent onHeldItemChanged;

    #region setup
    private void Awake()
    {
        items = new BaseItem[inventorySize];
        for (int i = 0; i < inventorySize; i++)
        {
            items[i] = null;
        }
    }

    private void SetupInventoryItem()
    {

    }
    #endregion setup

    #region interface
    public bool AddItem(BaseItem itemPrefab)
    {
        int firstAvailable = GetFirstOpenInv();

        if(firstAvailable < 0) { return false; }//full

        Vector2Int gridPos = GetXYFromIndex(firstAvailable);

        var g = Instantiate(itemPrefab, transform);
        g.transform.localPosition = GetUIPosition(gridPos);


        BaseItem item = g.GetComponent<BaseItem>();
        if(item == null)
        {
            Debug.LogError("adding non item to inventory");
        }


        item.Setup(this);
        items[firstAvailable] = item;

        return true;   
    }

    public void RemoveItem(BaseItem item)
    {
        int ind = FindIndexOf(item);

        if(ind < 0) { Debug.LogWarning("trying to remove an item that isnt found in the inventory!"); }

        items[ind] = null;
        Destroy(item.gameObject);
    }

    public void EquipItem(HeldItem item)
    {
        if(currentHeldItem == item)
        {
            currentHeldItem = null;
        }
        else 
        { 
            currentHeldItem = item;
        }

        onHeldItemChanged.Invoke(this, currentHeldItem);
    }

    public HeldItem GetCurrentHeldItem()
    {
        return currentHeldItem;
    }
    #endregion interface

    #region helpers 

    private Vector2 GetPositionFromIndex(int index)
    {
        return GetUIPosition(GetXYFromIndex(index));
    }
    private int GetFirstOpenInv()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (items[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    private int FindIndexOf(BaseItem b)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (items[i] == b)
            {
                return i;

            }
        }
        return -1;
    }

    private Vector2Int GetXYFromIndex(int index)
    {
        return new Vector2Int(index % inventoryWidth, Mathf.FloorToInt(index / inventoryWidth));
    }
    private Vector2 GetUIPosition(Vector2Int v)
    {
        return new Vector2(v.x * 72 + 8, -(v.y * 72 + 8));
    }

    private int GetIndexFromXY(Vector2Int xy)
    {
        return inventoryWidth * xy.y + xy.x;
    }
    private Vector2Int GetGridPosition(Vector2 v)
    {
        int x = Mathf.FloorToInt((v.x - 0) / 80);
        int y = Mathf.FloorToInt(-(v.y - 0) / 80);

        return new Vector2Int(x, y);
    }

    private Vector2 GetLocalPointFromScreenPos(Vector2 p)
    {
        Vector2 pos;
        bool hit = RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, p, null, out pos);
        return pos;
    }

    #endregion helpers



    #region EventHandlers


    private int initialSwapIndex;
    public void OnBeginDrag(PointerEventData eventData)
    {

        initialSwapIndex = GetIndexFromXY(
                            GetGridPosition(
                                GetLocalPointFromScreenPos(eventData.position)
                                )
                            );

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("go");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int finalSwapIndex = GetIndexFromXY(
                            GetGridPosition(
                                GetLocalPointFromScreenPos(eventData.position)
                                )
                            );

        SwapEntries(initialSwapIndex, finalSwapIndex);

        initialSwapIndex = -1;
    }
    #endregion EventHandlers
    private void SwapEntries(int index1, int index2)
    {
        if(index1 < 0 || index1 >=  inventorySize) { return; }
        if(index2 < 0 || index2 >=  inventorySize) { return; }

        if(index1 == index2) { return; }

        if(items[index1] == null && items[index2] == null) { return; }


        if(items[index1] != null)
        {
            items[index1].transform.localPosition = GetPositionFromIndex(index2);
        }

        if (items[index2] != null)
        {
            items[index2].transform.localPosition = GetPositionFromIndex(index1);
        }

        BaseItem tmp = items[index1];

        items[index1] = items[index2];
        items[index2] = tmp;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(GetIndexFromXY(
                            GetGridPosition(
                                GetLocalPointFromScreenPos(eventData.position)
                                )
                            ));
    }

    public void HandleItemUse(BaseItem b)
    {
        b.Use();
    }
}
