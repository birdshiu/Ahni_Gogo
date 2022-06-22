using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeat : MonoBehaviour
{
    private BoxCollider boxCollider;
    private Vector3 originalPosition;
    private float repeatCheckPointX;

    void Start()
    {
        originalPosition = transform.position;
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if(originalPosition.x-transform.position.x >= boxCollider.size.x/2)
        {
            transform.position = originalPosition;
        }
    }
}
