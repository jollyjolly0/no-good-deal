using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIData",menuName = "ScriptableObjects/AIScriptableObject",order = 1)]
public class AIScriptableObject : ScriptableObject
{
    public float curiousityLevel;
    public float snitchLevel;
    public float walkSpeed;
    public float runSpeed;
    public float maxTimeInShop;
    public float maxTimeLooking;
}
