using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El jugador
    public Vector3 offset;   // Offset opcional
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (target == null) return;

        // Movimiento en X con suavizado
        float newX = Mathf.Lerp(transform.position.x, target.position.x + offset.x, smoothSpeed);

        // Movimiento en Y solo si el jugador está más alto
        float targetY = target.position.y + offset.y;
        float newY = transform.position.y;

        if (targetY > transform.position.y)
        {
            newY = Mathf.Lerp(transform.position.y, targetY, smoothSpeed);
        }

        // Z se mantiene igual
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
