using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager3rd : MonoBehaviour
{
    InputManager3rd inputManager3rd; // to reference the InputManager script
    PlayerMovement3rd playerMovement3rd; // to reference the PlayerMovement3rd script

    private void Awake()
    {
        inputManager3rd = GetComponent<InputManager3rd>(); // get the InputManager3rd component
        playerMovement3rd = GetComponent<PlayerMovement3rd>(); // get the PlayerMovement3rd component
    }

    private void Update()
    {
        inputManager3rd.HandleAllInput(); // call HandleAllInputs() on inputManager3rd
    }

    // Fixed Update update at a fixed step defined in the editor
    // Recommended for all physics and collisions that rely on time intervals
    private void FixedUpdate()
    {
        // call the function that handles all movements
        playerMovement3rd.HandleAllMovement();
    }
}

