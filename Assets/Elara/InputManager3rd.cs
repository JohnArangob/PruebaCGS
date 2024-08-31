using UnityEngine;

public class InputManager3rd : MonoBehaviour
{
    PlayerControls3rd playerControls;
    public Vector2 movementInput;

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls3rd();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Propiedades para acceder a los inputs de movimiento vertical y horizontal
    public float verticalInput
    {
        get { return movementInput.y; }
    }

    public float horizontalInput
    {
        get { return movementInput.x; }
    }

    public void HandleAllInput()
    {
        // Llamamos a otros métodos de manejo de inputs
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        // Aquí puedes manejar los inputs si necesitas hacer más cosas.
    }
}
