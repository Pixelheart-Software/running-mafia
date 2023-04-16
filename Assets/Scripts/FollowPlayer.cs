using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowPlayer : MonoBehaviour
{
    public Transform followSubject;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;
        var position = followSubject.transform.position;
        newPos.x = position.x;
        newPos.y = position.y;
        transform.position = Vector3.Lerp(transform.position, newPos + offset, 0.1f);
    }
}
