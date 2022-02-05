using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayering : MonoBehaviour
{
    [SerializeField]
    Transform parentTransform;

    [SerializeField]
    private float minZ;

    [SerializeField]
    private float maxZ;

    private void Awake()
    {
        parentTransform = this.transform.parent;
    }

    private void Update()
    {
        float yPos = parentTransform.transform.position.z - minZ + .1f;
        this.transform.position = new Vector3(this.transform.position.x, yPos, this.transform.position.z);
    }
}
