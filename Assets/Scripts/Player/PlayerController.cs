using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    [SerializeField] private float jumpSpeed;

    private Animator _animator;
    private GameControls _gameControls;
    private CharacterController _characterController;

    private readonly int _rightPunchTriggerAnimatorHash = Animator.StringToHash("RightPunchTrigger");
    private readonly int _leftPunchTriggerAnimatorHash = Animator.StringToHash("LeftPunchTrigger");
    private readonly int _jumpTriggerAnimatorHash = Animator.StringToHash("JumpTrigger");
    private readonly int _rollTriggerAnimatorHash = Animator.StringToHash("RollTrigger");
    private readonly int _isRollingAnimatorHash = Animator.StringToHash("IsRolling");
    private readonly int _groundedAnimatorHash = Animator.StringToHash("Grounded");

    private int _currentPunchAnimatorHash;

    private float _originalZ;
    private Vector3 _velocity;
    private float _originalCharacterControllerHeight;
    private float _originalCharacterControllerCenterY;
    private bool _isRolling;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _originalCharacterControllerHeight = _characterController.height;
        _originalCharacterControllerCenterY = _characterController.center.y;

        _gameControls = new GameControls();
        _gameControls.PlayerControls.Jump.started += OnJump;
        _gameControls.PlayerControls.Roll.started += OnRoll;

        _currentPunchAnimatorHash = _rightPunchTriggerAnimatorHash;

        _originalZ = transform.position.z;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (_characterController.isGrounded && !_isRolling)
        {
            _animator.SetTrigger(_jumpTriggerAnimatorHash);
            _velocity.y = jumpSpeed;
        }
    }

    private void OnRoll(InputAction.CallbackContext context)
    {
        if (_characterController.isGrounded && !_isRolling)
        {
            _animator.SetTrigger(_rollTriggerAnimatorHash);
            _characterController.height = _originalCharacterControllerHeight * 0.5f;

            var characterControllerCenter = _characterController.center;
            characterControllerCenter.y = _originalCharacterControllerCenterY;
            characterControllerCenter.y += (_characterController.height - _originalCharacterControllerHeight) * 0.5f;
            _characterController.center = characterControllerCenter;
        }
    }

    void Update()
    {
        if (_isRolling && !_animator.GetBool(_isRollingAnimatorHash))
        {
            GetUpAfterRoll();
        }

        _isRolling = _animator.GetBool(_isRollingAnimatorHash);
        _velocity.x = movementSpeed;

        if (!_characterController.isGrounded)
        {
            _velocity.y += Physics.gravity.y * Time.deltaTime;
        }

        _characterController.Move(_velocity * Time.deltaTime);
        _animator.SetBool(_groundedAnimatorHash, _characterController.isGrounded);
    }

    private void LateUpdate()
    {
        var transformPosition = transform.position;
        transformPosition.z = _originalZ;
        transform.position = transformPosition;
    }

    private void OnEnable()
    {
        _gameControls.PlayerControls.Enable();
    }

    private void OnDisable()
    {
        _gameControls.PlayerControls.Disable();
    }

    public void DoKick(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (!enemy.IsDead())
        {
            _animator.SetTrigger(_currentPunchAnimatorHash);
        }
    }

    public void DoPunch(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (!enemy.IsDead())
        {
            _animator.SetTrigger(_currentPunchAnimatorHash);
        }
    }

    private void OnDestroy()
    {
        _gameControls.PlayerControls.Jump.started -= OnJump;
        _gameControls.PlayerControls.Roll.started -= OnRoll;
    }

    public void GetUpAfterRoll()
    {
        _characterController.height = _originalCharacterControllerHeight;
        var characterControllerCenter = _characterController.center;
        characterControllerCenter.y = _originalCharacterControllerCenterY;
        _characterController.center = characterControllerCenter;
    }

    public void LeftHandReady()
    {
        _currentPunchAnimatorHash = _leftPunchTriggerAnimatorHash;
    }

    public void RightHandReady()
    {
        _currentPunchAnimatorHash = _rightPunchTriggerAnimatorHash;
    }
}