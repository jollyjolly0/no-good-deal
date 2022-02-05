using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItem : MonoBehaviour
{
    public List<Sprite> HeldSprites;
    [SerializeField]
    private SpriteRenderer currentlyHeld;

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
