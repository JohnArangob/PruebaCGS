using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;  // Asignar el objeto jugador
    public Vector3 offset;    // Offset de la cámara respecto al jugador

    void LateUpdate()
    {
        // Posiciona la cámara en la posición del jugador más el offset
        transform.position = player.position + offset;
        transform.LookAt(player);  // Asegura que la cámara siempre mire hacia el jugador
    }
}


