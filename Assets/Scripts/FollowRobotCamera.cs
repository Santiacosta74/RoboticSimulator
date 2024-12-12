using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform robot;  // Referencia al robot
    public Vector3 offset = new Vector3(0f, 2f, -5f);  // Distancia de la cámara al robot
    public float rotationSpeed = 5f;  // Velocidad de rotación de la cámara

    private void LateUpdate()
    {
        // Asegurarse de que la cámara sigue al robot con la distancia y rotación correctas.
        FollowRobot();
    }

    void FollowRobot()
    {
        // La posición de la cámara sigue la posición del robot + un offset (desplazamiento)
        transform.position = robot.position + offset;

        // Obtener la rotación deseada de la cámara basada en la rotación del robot.
        Quaternion targetRotation = Quaternion.Euler(0, robot.eulerAngles.y, 0); // Mantener solo la rotación en el eje Y

        // Suavizar la rotación de la cámara para que siga al robot de manera suave.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
