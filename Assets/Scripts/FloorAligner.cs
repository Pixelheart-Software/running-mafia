using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class FloorAligner : MonoBehaviour
{
    private const float SnapDistance = 0.2F;

    public PositionableScriptableObject objectDefinition;

    [FormerlySerializedAs("florToTheLeft")] [SerializeField]
    public Transform floorToTheLeft;

    [FormerlySerializedAs("OverridePositionZ")]
    public bool overridePositionZ;

    [FormerlySerializedAs("PositionZOverride")]
    public float positionZOverride;

    private Vector3 _lastSetPosition;
    private float _lastPositionZOverride;
    private Vector3 _leftDirection;

    private void Awake()
    {
        _leftDirection = transform.right * -1;

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

        floorToTheLeft = FindFloorToLeft(bounds.center);

        DrawDebugLines(bounds.center);

        _lastSetPosition = transform.position;

        if (floorToTheLeft != null)
        {
            if (Vector3.Distance(floorToTheLeft.transform.position, transform.position) < (bounds.size.x + SnapDistance))
            {
                Vector3 newPosition = floorToTheLeft.transform.position;
                newPosition.x += bounds.size.x;

                _lastSetPosition.x = newPosition.x;
            }
        }

        _lastSetPosition.y = objectDefinition.Elevation;
        _lastSetPosition.z = overridePositionZ
            ? positionZOverride
            : (bounds.size.z / 2);

        transform.position = _lastSetPosition;
    }

    private void DrawDebugLines(Vector3 center)
    {
        if (floorToTheLeft != null)
        {
            Debug.DrawLine(center, floorToTheLeft.GetComponent<Collider>().bounds.center);
        }
        else
        {
            Debug.DrawRay(center, _leftDirection * 100, Color.red);
        }
    }


    private Transform FindFloorToLeft(Vector3 center)
    {
        Ray ray = new Ray(center, _leftDirection);

        if (Physics.Raycast(ray, out var hitData, Mathf.Infinity, LayerMask.GetMask("Floor")))
        {
            return hitData.transform;
        }

        return null;
    }
}