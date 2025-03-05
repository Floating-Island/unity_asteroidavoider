using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
