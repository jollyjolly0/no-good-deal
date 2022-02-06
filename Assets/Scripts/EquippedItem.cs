using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItem : MonoBehaviour
{
    public List<Sprite> HeldSprites;
    [SerializeField] private SpriteRenderer currentlyHeld;

    [SerializeField] private HeldItemEvent heldItemChanged;
    public HeldItem myHeldItem;

    private void Awake()
    {
        heldItemChanged.Fired += HeldItemChanged_Fired;
    }

    private void HeldItemChanged_Fired(object sender, HeldItem e)
    {
        myHeldItem = e;
        if (e == null)
        {
            StopHolding();
        }
        else
        {
            SetHolding(e.holdSprite);
        }
    }

    public void SetHolding(int spriteIndex)
    {
        currentlyHeld.sprite = HeldSprites[spriteIndex];
    }

    public void SetHolding(Sprite holdSprite)
    {
        currentlyHeld.sprite = holdSprite;
    }

    public void StopHolding()
    {
        SetHolding(0);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetHolding(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetHolding(1);
        }
    }
}
