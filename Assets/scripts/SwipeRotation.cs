using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeRotation : MonoBehaviour
{
    private PlayerController controller;
    [SerializeField] private LayerMask pickableLayer;

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    public float rotationSpeed = 50f;
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;

            ClickOnObject();
        }

        if (Input.GetMouseButton(0))
        {
            currentTouchPosition = Input.mousePosition;
            float swipeDirection = startTouchPosition.x - currentTouchPosition.x;
            transform.Rotate(0, swipeDirection * Time.deltaTime * rotationSpeed, 0);
            startTouchPosition = currentTouchPosition;
        }
    }

    private void ClickOnObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, pickableLayer))
        {
            if (hit.collider.CompareTag("Coin"))
            {
                controller.PickUpCoin();
            }

            Destroy(hit.transform.gameObject);
        }
    }
}