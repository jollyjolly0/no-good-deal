using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualBowlInteractable : BaseInteractable
{
    public bool filled;

    public SpriteRenderer spriteRenderer;

    public Sprite filledSprite;

    public override string GetActionName()
    {
        return "Fill Bowl";
    }
    public override void Interact(GameObject actor)
    {
        base.Interact(actor);
        spriteRenderer.sprite = filledSprite;
        filled = true;
    }
}
