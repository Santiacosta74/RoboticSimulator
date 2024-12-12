using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform robot;  // Referencia al robot
    public Vector3 offset = new Vector3(0f, 2f, -5f);  // Distancia de la c�mara al robot
    public float rotationSpeed = 5f;  // Velocidad de rotaci�n de la c�mara

    private void LateUpdate()
    {
        // Asegurarse de que la c�mara sigue al robot con la distancia y rotaci�n correctas.
        FollowRobot();
    }

    void FollowRobot()
    {
        // La posici�n de la c�mara sigue la posici�n del robot + un offset (desplazamiento)
        transform.position = robot.position + offset;

        // Obtener la rotaci�n deseada de la c�mara basada en la rotaci�n del robot.
        Quaternion targetRotation = Quaternion.Euler(0, robot.eulerAngles.y, 0); // Mantener solo la rotaci�n en el eje Y

        // Suavizar la rotaci�n de la c�mara para que siga al robot de manera suave.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
