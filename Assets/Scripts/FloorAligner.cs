using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class FloorAligner : MonoBehaviour
{
    public PositionableScriptableObject objectDefinition;
    
    public bool overridePositionZ;
    public float positionZOverride;

    private Vector3 _lastSetPosition;
    private float _lastPositionZOverride;

    private void Awake()
    {
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
        
        _lastSetPosition = transform.position;

        _lastSetPosition.y = objectDefinition.Elevation;
        _lastSetPosition.z = overridePositionZ
            ? positionZOverride
            : (bounds.size.z / 2);

        transform.position = _lastSetPosition;
    }
}
