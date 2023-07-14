using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class AlignLeft : MonoBehaviour
{
    private const float SnapDistance = 0.4F;
    
    [SerializeField]
    private LayerMask layerMask;
    
    private Vector3 _leftDirection;
    private Vector3 _lastSetPosition;

    private void Awake()
    {
        _leftDirection = transform.right * -1;
    }

    private void Update()
    {
        if (_lastSetPosition != transform.position)
        {
            Align();
        }
    }

    private void OnValidate()
    {
        Align();
    }

    private void Align()
    {
        var bounds = GetComponent<Collider>().bounds;
        var objectToLeft = FindObjectToLeft(bounds.center);
        Vector3 position = transform.position;

        DrawDebugLines(bounds.center, objectToLeft);

        _lastSetPosition = transform.position;
        
        if (objectToLeft != null)
        {
            var otherBounds = objectToLeft.GetComponent<Collider>().bounds;

            if (Vector3.Distance(objectToLeft.transform.position, transform.position) < (otherBounds.size.x + SnapDistance))
            {
                Vector3 newPosition = objectToLeft.transform.position;
                newPosition.x += otherBounds.size.x;

                position.x = newPosition.x;
            }
        }

        transform.position = position;
    }
    
    private Transform FindObjectToLeft(Vector3 center)
    {
        Ray ray = new Ray(center, _leftDirection);

        //if (Physics.Raycast(ray, out var hitData, Mathf.Infinity, LayerMask.GetMask("Floor")))
        if (Physics.Raycast(ray, out var hitData, Mathf.Infinity, layerMask))
        {
            return hitData.transform;
        }

        return null;
    }
    
    private void DrawDebugLines(Vector3 center, Transform objectToLeft)
    {
        if (objectToLeft != null)
        {
            Debug.DrawLine(center, objectToLeft.GetComponent<Collider>().bounds.center);
        }
        else
        {
            Debug.DrawRay(center, _leftDirection * 100, Color.red);
        }
    }
}
