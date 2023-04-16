using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    
    [SerializeField]
    private float jumpSpeed;

    private Animator _animator;
    private GameControls _gameControls;
    private CharacterController _characterController;

    private int _punchAnimatorHash;
    private int _jumpAnimatorHash;
    private int _groundedAnimatorHash;

    private Vector3 _velocity;

    private void Awake()
    {
        _punchAnimatorHash = Animator.StringToHash("Punch");
        _jumpAnimatorHash = Animator.StringToHash("Jump");
        _groundedAnimatorHash = Animator.StringToHash("Grounded");

        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        
        _gameControls = new GameControls();
        _gameControls.PlayerControls.Jump.started += OnJump;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (_characterController.isGrounded)
        {
            _animator.SetTrigger(_jumpAnimatorHash);
            _velocity.y = jumpSpeed;
        }
    }

    void Update()
    {
        _velocity.x = movementSpeed;

        if (!_characterController.isGrounded)
        {
            _velocity.y += Physics.gravity.y * Time.deltaTime;
        }

        _characterController.Move(_velocity * Time.deltaTime);
        _animator.SetBool(_groundedAnimatorHash, _characterController.isGrounded);
    }

    private void OnEnable()
    {
        _gameControls.PlayerControls.Enable();
    }

    private void OnDisable()
    {
        _gameControls.PlayerControls.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!other.GetComponent<EnemyController>().IsDead())
            {
                _animator.SetTrigger(_punchAnimatorHash);
            }
        }
    }

    private void OnDestroy()
    {
        _gameControls.PlayerControls.Jump.started -= OnJump;
    }
}
