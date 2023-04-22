using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
[RequireComponent(typeof(ObstacleController))]
public class ObstacleAligner : MonoBehaviour
{
    private ObstacleController _controller;
    
    [SerializeField]
    public Transform _parentFloor;

    private Vector3 _downDirection;

    private void Awake()
    {
        _controller = GetComponent<ObstacleController>();
        _downDirection = transform.up * -1;
    }

    private void Update()
    {
        Align();
    }
    
    private void Align()
    {
        var bounds = GetComponent<BoxCollider>().bounds;
        Vector3 center = bounds.center;
        
        FindRelatedFloor(center);

        DrawDebugLines(center);

        if (_parentFloor == null)
        {
            return;
        }

        var transformPosition = transform.position;
        switch (_controller.obstacleDefinition.type)
        {
            case ObstacleType.HIGH:
                transformPosition.y = _parentFloor.position.y + ObstacleController.HIGH_HEIGHT;
                break;
            case ObstacleType.LOW:
                transformPosition.y = _parentFloor.position.y + ObstacleController.LOW_HEIGHT;
                break;
            default:
                // TODO
                break;
        }

        transformPosition.z = _parentFloor.position.z + (bounds.size.z / 2);

        transform.position = transformPosition;
    }

    private void DrawDebugLines(Vector3 center)
    {
        if (_parentFloor != null)
        {
            Debug.DrawLine(center, _parentFloor.GetComponent<Renderer>().bounds.center);
        }
        else
        {
            Debug.DrawRay(center, _downDirection * 10, Color.red);
        }
    }

    private void FindRelatedFloor(Vector3 center)
    {
        Ray ray = new Ray(center, transform.up * -1);

        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData, Mathf.Infinity, LayerMask.GetMask("Floor")))
        {
            _parentFloor = hitData.transform;
        }
        else
        {
            _parentFloor = null;
        }
    }
}
