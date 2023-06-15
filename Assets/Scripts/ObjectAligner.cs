using DefaultNamespace;
using UnityEngine;

[ExecuteInEditMode]
public class ObjectAligner : MonoBehaviour
{
    public PositionableScriptableObject objectDefinition;
    
    [SerializeField]
    public Transform parentFloor;

    public bool OverridePositionZ = false;
    public float PositionZOverride = 0;

    private Vector3 _downDirection;
    private Vector3 _lastSetPosition;

    private void Awake()
    {
        _downDirection = transform.up * -1;

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
        _lastSetPosition.y = parentFloor.position.y + objectDefinition.Elevation;
        _lastSetPosition.z = OverridePositionZ
            ? PositionZOverride
            : parentFloor.position.z + (bounds.size.z / 2);

        transform.position = _lastSetPosition;
    }

    private void DrawDebugLines(Vector3 center)
    {
        if (parentFloor != null)
        {
            Debug.DrawLine(center, parentFloor.GetComponent<Renderer>().bounds.center);
        }
        else
        {
            Debug.DrawRay(center, _downDirection * 10, Color.red);
        }
    }

    private Transform FindRelatedFloor(Vector3 center)
    {
        Ray ray = new Ray(center, transform.up * -1);

        if (Physics.Raycast(ray, out var hitData, Mathf.Infinity, LayerMask.GetMask("Floor")))
        {
            return hitData.transform;
        }
        return null;
    }
}
