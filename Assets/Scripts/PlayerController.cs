using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DangerousPenguin.Input;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace DangerousPenguin
{

public class PlayerController : MonoBehaviour
{
    private static readonly int MoveSpeedAnimatorProperty = Animator.StringToHash("MoveSpeed");

    private Camera       _camera;
    private Vector3      _moveTarget = Vector3.zero;
    private bool         _moving     = false;
    private GameControls _input;
    private NavMeshPath  _path;

    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float     moveSpeed;
    [SerializeField] private float     rotationSpeed;
    [SerializeField] private Animator  animator;
    [SerializeField] private double    maxDeviationFromPath = 0.2;

    #region Mouse features

    [SerializeField] private MoveIndicator moveIndicator;

    #endregion

    // Update is called once per frame
    private void Start()
    {
        _camera = Camera.main;
        _input  = new GameControls();
        _input.Game.Enable();
        _path = new NavMeshPath();
    }

    void Update()
    {
        HandleMouseInput();


        HandleMovement();
    }

    private void HandleMovement()
    {
        var magnitude                 = (_moveTarget - transform.position).magnitude;
        if (magnitude < 0.1f) _moving = false;
        animator.SetFloat(MoveSpeedAnimatorProperty, _moving ? 15.0f : 0.0f, 0.1f, Time.deltaTime);

        if (!_moving) return;

        NavMesh.CalculatePath(transform.position, _moveTarget, NavMesh.AllAreas, _path);
        var navMagnitude = 0.0f;
        for (int i = 0; i < _path.corners.Length - 1; i++)
        {
            navMagnitude += (_path.corners[i] - _path.corners[i + 1]).magnitude;
        }

        var nextPoint = _moveTarget;
        
        if (_path.corners.Any() && navMagnitude < magnitude * (1.0f+maxDeviationFromPath))
        {
            var nextPotentialPoint = _path.corners.First();
            if ((nextPotentialPoint - transform.position).magnitude < 0.1f)
            {
                if (_path.corners.Length > 1) nextPoint = _path.corners[1];
            }
            else
            {
                nextPoint = nextPotentialPoint;
            }
        }

        var direction = (nextPoint - transform.position).normalized;
        transform.rotation =  Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void HandleMouseInput()
    {
        if (!_input.Game.Move.IsPressed()) return;
        if (!GetMouseLocation(out var mousePosition)) return; //mouse outside ground

        _moving     = true;
        _moveTarget = mousePosition;
        if (_input.Game.Move.WasPressedThisFrame())
            moveIndicator.PingLocation(mousePosition);
    }

    private bool GetMouseLocation(out Vector3 position)
    {
        var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out var hit, 100.0f, groundLayerMask))
        {
            position = hit.point;
            return true;
        }

        position = Vector3.negativeInfinity;
        return false;
    }

    private void OnDrawGizmos()
    {
        if (_path is null) return;
        var sb = new StringBuilder();
        for (int i = 0; i < _path.corners.Length - 1; i++)
        {
            sb.AppendLine(_path.corners[i].ToString());
            Debug.DrawLine(_path.corners[i], _path.corners[i + 1], (i % 2 == 0) ? Color.red : Color.green);
        }
        // Debug.Log(sb.ToString());
    }
}

}