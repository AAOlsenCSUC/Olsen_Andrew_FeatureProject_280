using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Andrew Olsen
 * Last Updated: 12/07/2023
 * Controls player movement
 */
public class PlayerMovement : MonoBehaviour
{
    public InputManager inputManager;

    public Vector3 moveDirection;
    public Transform cameraObject;
    public Rigidbody playerRigidbody;

    public float movementSpeed;
    public float rotationSpeed = 15;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    /// <summary>
    /// Handles all movement controls for the player.
    /// </summary>
    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    /// <summary>
    /// Controls the player's directional movement.
    /// </summary>
    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * movementSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;
    }

    /// <summary>
    /// Controls the player's rotational movement.
    /// </summary>
    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
}
