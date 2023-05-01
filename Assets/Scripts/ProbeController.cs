using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils;

[RequireComponent(typeof(Collider))]
public class ProbeController : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<Collider> @event;
    
    [TagSelector]
    public string probeForTag = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(probeForTag))
        {
            @event.Invoke(other);
        }
    }
}
