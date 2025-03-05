using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float forceMagnitude = 10f;
    [SerializeField] private float maxSpeed = 500f;
    private Camera mainCamera;
    private Vector2 movementDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    private Vector3 TouchWorldPosition()
    {
        Vector2 touchPositionsSum = SumTouchPositions();

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPositionsSum);

        return worldPosition;
    }

    private Vector2 SumTouchPositions()
    {
        Vector2 touchPositionsSum = new Vector2();
        foreach (Touch touch in Touch.activeTouches)
        {
            touchPositionsSum += touch.screenPosition;
        }

        touchPositionsSum /= Touch.activeTouches.Count;
        return touchPositionsSum;
    }

    private void UpdateMovementDirection()
    {
        movementDirection =  (TouchWorldPosition() - playerRigidbody.transform.position).normalized;
    }

    private void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            UpdateMovementDirection();
        }
    }

    // We use FixedUpdate for physics calculations
    private void FixedUpdate()
    {
        AddForceToRigidBody();
        LimitSpeed();
    }

    private void AddForceToRigidBody()
    {
        playerRigidbody.AddForce(movementDirection * forceMagnitude, ForceMode.Force);
    }

    private void LimitSpeed()
    {
        if (playerRigidbody.linearVelocity.magnitude > maxSpeed)
        {
            playerRigidbody.linearVelocity = playerRigidbody.linearVelocity.normalized * maxSpeed;
        }
    }
}
