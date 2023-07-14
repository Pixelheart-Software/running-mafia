using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class ObjectAligner : MonoBehaviour
{
    public PositionableScriptableObject objectDefinition;
    
    [SerializeField]
    public Transform parentFloor;

    public bool overridePositionZ = false;
    public float positionZOverride = 0;
    public bool overridePositionY = false;

    private Vector3 _downDirection;
    private Vector3 _lastSetPosition;

    private void Awake()
    {
        _downDirection = transform.up * -1;

        Align();
    }

    private void OnValidate()
    {
        Align();
    }

    private void Update()
    {
        if (_lastSetPosition != transform.position)
        {
            Align();
        }
    }
    
    private void Align()
    {
        var bounds = GetComponent<Collider>().bounds;

        parentFloor = FindRelatedFloor(bounds.center);

        DrawDebugLines(bounds.center);

        if (parentFloor == null)
        {
            return;
        }

        _lastSetPosition = transform.position;

        if (!overridePositionY)
        {
            _lastSetPosition.y = parentFloor.position.y + objectDefinition.Elevation;
        }
        _lastSetPosition.z = overridePositionZ
            ? positionZOverride
            : parentFloor.position.z + (bounds.size.z / 2);

        transform.position = _lastSetPosition;
    }

    private void DrawDebugLines(Vector3 center)
    {
        if (!objectDefinition.IsFloor)
        {
            if (parentFloor != null)
            {
                Debug.DrawLine(center, parentFloor.GetComponent<Collider>().bounds.center);
            }
            else
            {
                Debug.DrawRay(center, _downDirection * 10, Color.red);
            }
        }
    }

    private Transform FindRelatedFloor(Vector3 center)
    {
        Ray ray = new Ray(center, _downDirection);

        if (Physics.Raycast(ray, out var hitData, Mathf.Infinity, LayerMask.GetMask("Floor")))
        {
            return hitData.transform;
        }
        return null;
    }
}
