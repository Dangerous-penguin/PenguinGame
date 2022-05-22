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
    private Vector3      _moveTargetCandidate = Vector3.zero;
    private Vector3      _moveTarget          = Vector3.zero;
    private bool         _moving              = false;
    private GameControls _input;
    private NavMeshPath  _path;

    private LoopedArray<Vector3> _lastMoves = new(10);

    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform cameraHandle;
    [SerializeField] private Animator  animator;
    [SerializeField] private float     moveSpeed            = 10.0f;
    [SerializeField] private float     rotationSpeed        = 20.0f;
    [SerializeField] private double    maxDeviationFromPath = 0.2;
    [SerializeField] private float     minDistanceCutoff    = 0.1f;
    [SerializeField] private float     cameraMoveSpeed      = 10.0f;


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
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        cameraHandle.position = Vector3.Lerp(cameraHandle.position, transform.position, cameraMoveSpeed * Time.deltaTime);
    }

    private void HandleMovement()
    {
        var magnitude                 = (_moveTargetCandidate - transform.position).magnitude;
        if (magnitude < 0.2f) _moving = false;

        if (!_moving)
        {
            animator.SetFloat(MoveSpeedAnimatorProperty, _moving ? 15.0f : 0.0f, 0.1f, Time.deltaTime);
            return;
        }

        NavMesh.CalculatePath(transform.position, _moveTargetCandidate, NavMesh.AllAreas, _path);
        if (!_path.corners.Any())
            NavMesh.CalculatePath(transform.position, _moveTarget, NavMesh.AllAreas, _path);

        if (lineRenderer)
        {
            //TODO: remove. debug lines
            lineRenderer.positionCount = _path.corners.Length;
            lineRenderer.SetPositions(_path.corners);
        }

        var navMagnitude = 0.0f;
        for (int i = 0; i < _path.corners.Length - 1; i++)
        {
            navMagnitude += (_path.corners[i] - _path.corners[i + 1]).magnitude;
        }

        if (navMagnitude < 0.2f) _moving = false;

        var nextPoint = _moveTargetCandidate;

        if (_path.corners.Any())
        {
            if (navMagnitude < magnitude * (1.0f + maxDeviationFromPath))
            {
                var nextPotentialPoint = _path.corners.First();
                if ((nextPotentialPoint - transform.position).magnitude < 0.1f)
                {
                    if (_path.corners.Length > 1)
                    {
                        nextPoint = _path.corners[1];
                    }
                }
                else
                {
                    nextPoint = nextPotentialPoint;
                }
            }

            _moveTarget = nextPoint;
        }
        else
        {
            //target is not reachable, revert to last good one
            nextPoint = _moveTarget;
        }

        if (_moving)
        {
            var direction = (nextPoint - transform.position).normalized;
            transform.rotation =  Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            transform.position += direction * moveSpeed * Time.deltaTime;

            //check if the player is stuck somewhere, stop moving if so
            _lastMoves.Push(transform.position);
            var lastMovesTotal = 0.0f;
            for (var index = 0; index < _lastMoves.Length - 1; index++)
            {
                var position     = _lastMoves[index];
                var nextPosition = _lastMoves[index + 1];
                lastMovesTotal += (nextPosition - position).magnitude;
            }

            if (lastMovesTotal < minDistanceCutoff) _moving = false;
        }

        animator.SetFloat(MoveSpeedAnimatorProperty, _moving ? 15.0f : 0.0f, 0.1f, Time.deltaTime);
    }

    private void HandleMouseInput()
    {
        if (!_input.Game.Move.IsPressed()) return;
        if (!GetMouseLocation(out var mousePosition)) return; //mouse outside ground


        _moving              = true;
        _moveTargetCandidate = mousePosition;
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
        for (int i = 0; i < _path.corners.Length - 1; i++)
        {
            Debug.DrawLine(_path.corners[i], _path.corners[i + 1], (i % 2 == 0) ? Color.red : Color.green);
        }
    }
}

}