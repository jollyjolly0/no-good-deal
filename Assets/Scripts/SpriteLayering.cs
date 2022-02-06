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
        float zPos = (-1f*(parentTransform.transform.position.z - minZ)) + .1f;
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, zPos);
    }
}
