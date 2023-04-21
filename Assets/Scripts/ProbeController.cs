using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utils;

[RequireComponent(typeof(Collider))]
public class ProbeController : MonoBehaviour
{
    [SerializeField]
    private UnityEvent @event;
    
    [TagSelector]
    public string probeForTag = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(probeForTag))
        {
            @event.Invoke();
        }
    }
}
