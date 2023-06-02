using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{



    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float runMultiplier = 1.2f;
    [SerializeField] private GameObject cameraObject;
    //Code beneath is for ground detection to keep the player grounded
    [SerializeField] public float maxSlopeAngle = 45f;
    [SerializeField] public float smoothingFactor = 0.1f;
    [SerializeField] public float raycastLength = 1.5f;
    [SerializeField] public float groundOffset = 0.1f;
    private Vector3 targetPosition;

    private CharacterController controller;
    private Transform mainCameraTransform;
    private Animator animator;

    private void Start()
    {

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {

        CharacterInput();
        CharacterGrounding();

    }

    void CharacterInput()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(moveHorizontal) > 0.1f || Mathf.Abs(moveVertical) > 0.1f)
        {
            // Calculate the movement vector based on input
            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
            movement = cameraObject.transform.TransformDirection(movement);
            movement.y = 0f;
            movement.Normalize();

            if (animator.GetBool("isWalking") && !(animator.GetBool("isRunning")))
            {
                controller.Move(movement * speed * Time.deltaTime);
            }

            if (animator.GetBool("isRunning"))
            {
                controller.Move(movement * speed * runMultiplier * Time.deltaTime);
            }

            // Rotate the player to face the same direction as the camera
            float cameraRotationY = cameraObject.transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, cameraRotationY, 0f);
        }

    }

    void CharacterGrounding()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        RaycastHit hit;
        Vector3 raycastOrigin = transform.position + Vector3.up * groundOffset; // Offset the raycast origin
        if (Physics.Raycast(raycastOrigin, Vector3.down, out hit, raycastLength))
        {

            if (hit.collider.CompareTag("Ground"))
            {

                float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

                if (slopeAngle <= maxSlopeAngle)
                {
                    targetPosition = hit.point + Vector3.up * groundOffset;
                }
                else
                {
                    // Adjust the movement direction to follow the slope
                    Vector3 movementDirection = new Vector3(moveHorizontal, 0f, moveVertical);
                    movementDirection = Vector3.ProjectOnPlane(movementDirection, hit.normal).normalized;

                    targetPosition = transform.position + movementDirection * speed * Time.deltaTime;
                }
            }

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothingFactor);

        }

    }
}

