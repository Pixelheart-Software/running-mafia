using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator _animator;

    private bool _dead;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PunchPoint"))
        {
            _animator.enabled = false;
            _dead = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.enabled = false;
        }
    }

    public bool IsDead()
    {
        return _dead;
    }
}
