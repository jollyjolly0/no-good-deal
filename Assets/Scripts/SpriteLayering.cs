using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayering : MonoBehaviour
{
    [SerializeField]
    Transform parentTransform;


    private const float minZ = -6f;


    private void Awake()
    {
        parentTransform = this.transform.parent;
    }

    private void Update()
    {
        float yPos = (-.05f*(parentTransform.transform.position.z - minZ)) + 1f;
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, yPos, this.transform.localPosition.z);
    }
}
