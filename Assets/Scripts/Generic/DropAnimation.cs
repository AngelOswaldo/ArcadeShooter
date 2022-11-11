using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    public float speedRotation;
    public float offset;
    public float speed;

    Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        Rotation();
        SinObject();
    }

    private void Rotation()
    {
        transform.Rotate(0f, Time.deltaTime + speedRotation, 0f);
    }

    private void SinObject()
    {
        transform.position = new Vector3(startPosition.x, 
            Mathf.Sin(Time.time * speed) * offset + startPosition.y, startPosition.z);
    }
}
