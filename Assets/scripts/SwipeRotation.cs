using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeRotation : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    public float rotationSpeed = 50f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            currentTouchPosition = Input.mousePosition;
            float swipeDirection = startTouchPosition.x - currentTouchPosition.x;
            transform.Rotate(0, swipeDirection * Time.deltaTime * rotationSpeed, 0);
            startTouchPosition = currentTouchPosition;
        }
    }
}
