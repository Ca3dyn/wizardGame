using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Animator animator;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float runMultiplier = 1.2f;
    [SerializeField] private GameObject cameraObject;

    private CharacterController controller;
    private Transform mainCameraTransform;

    private void Start() 
    {

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    void Update()
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
}

