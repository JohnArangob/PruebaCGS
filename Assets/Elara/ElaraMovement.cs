using UnityEngine;

public class ElaraMovement : MonoBehaviour
{
    public float moveSpeed = 20f;  // Velocidad de movimiento
    public float turnSpeed;  // Velocidad de giro
    public float jumpForce = 5f;  // Fuerza del salto

    private Animator animator; // Referencia al Animator
    private Rigidbody rb;      // Referencia al Rigidbody

    private bool isGrounded = true; // Para verificar si el personaje est� en el suelo

    private void Start()
    {
        // Obtener las referencias al Animator y Rigidbody
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        if (animator == null)
        {
            Debug.LogError("Animator component missing from this GameObject.");
        }
    }

    private void Update()
    {
        // Manejar el movimiento y las animaciones
        HandleMovement();
        HandleJump();
        HandleAnimations();
    }

    private void HandleMovement()
    {
        // Capturar entradas del teclado
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Crear un vector de movimiento basado en las entradas
        Vector3 movement = new Vector3(horizontal, 0, vertical);//.normalized;

        // Verificar si hay movimiento
        if (movement.magnitude >= 0.1f)
        {
            // Mueve al personaje
            rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);

            // Gira el personaje hacia la direcci�n de movimiento
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            // Activar animaci�n de correr
            animator.SetBool("isRunning", true);
        }
        else
        {
            // Detener la animaci�n de correr
            animator.SetBool("isRunning", false);
        }
    }

    private void HandleJump()
    {
        // Verificar si el personaje est� en el suelo antes de saltar
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Aplicar fuerza para el salto
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // El personaje ya no est� en el suelo

            // Activar la animaci�n de salto
            animator.SetTrigger("jump");
        }
    }

    private void HandleAnimations()
    {
        // Detectar el input para el ataque con Fire1 (clic izquierdo)
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire1 pressed - Kick Attack Triggered");
            animator.SetTrigger("kickAttack");
        }

        // Detectar el input para el ataque con Fire2 (clic derecho)
        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Fire2 pressed - Sword Attack Triggered");
            animator.SetTrigger("swordAttack");
        }
    }

    // M�todo para detectar cuando el personaje toca el suelo
    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si el personaje ha tocado el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // M�todo que puede ser llamado por otro script para activar la animaci�n de recibir un golpe
    public void ReceiveHit()
    {
        animator.SetTrigger("receiveHit"); // Activar la animaci�n de recibir un golpe
    }
}
