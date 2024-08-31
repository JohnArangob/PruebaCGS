using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3rd : MonoBehaviour
{
    InputManager3rd inputManager; // to reference the InputManager Script
    Vector3 moveDirection; // to store the direction of the movement
    Transform cameraObject; // to reference the camera
    Rigidbody playerRigidbody; // to reference a Rigidbody component to handle physics
    public float movementSpeed = 7; // to control the movement speed
    public float rotationSpeed = 15; // to control the rotation speed


    private void Awake()
    {
        inputManager = GetComponent<InputManager3rd>(); // get the InputManager3rd component
        playerRigidbody = GetComponent<Rigidbody>(); // get the Rigidbody component
        cameraObject = Camera.main.transform; // scan the scene for the main camera
       


    }


    // Update is called once per frame
    private void HandleMovement()
    {
        // Move player in the direction camera is facing and multiply for VerticalInput
        // W(back) & S(forward) keys
        moveDirection = cameraObject.forward * inputManager.verticalInput;

        // add the horizontal input to the moveDirection. A (left) y D (right)
        moveDirection += moveDirection + cameraObject.right * inputManager.horizontalInput;

        moveDirection.y = 0; // so the player cannot move to the sky!

        // Keep the vector the same direction but changes its magnitude to 1 to keep the same acceleration
        // in all directions, even in diagonals.
        moveDirection.Normalize();

        moveDirection *= movementSpeed; // to control the speed of the movement from the editor

        // store the final direction and speed of the movement in a new variable movementVelocity
        Vector3 movementVelocity = moveDirection;

        // applies the previous speed calculation to the velocity of the rigidbody
        playerRigidbody.velocity = movementVelocity;
    }


    private void HandleRotation() //"the idea is to face first the direction you want to move, and then move"
    {
        Vector3 targetDirection = Vector3.zero; // a new variable to store the rotation the player will rotate

        targetDirection = cameraObject.forward * inputManager.verticalInput;

        // add the horizontal input to the moveDirection. A (left) y D (right)
        targetDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;

        targetDirection.y = 0; // so the player cannot move to the sky!

        // keep the vector the same direction but changes its magnitude to 1 to
        // keep the same acceleration in all directions, even in diagonals.
        targetDirection.Normalize();

        // to look to our target rotation
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Do the rotation between the current orientation and the target rotation using a rotationSpeed * frame independent
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation; // applies the final rotation to the player
    }
    
    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }
}
