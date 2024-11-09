using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // Ссылка на объект игрока
    [SerializeField] private float smoothSpeed = 0.125f; // Скорость сглаживания
    [SerializeField] private Vector3 offset; // Смещение камеры относительно игрока

    private void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
