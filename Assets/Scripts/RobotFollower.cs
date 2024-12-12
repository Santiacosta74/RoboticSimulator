// RobotFollower.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFollower : MonoBehaviour
{
    public LineRenderer lineRenderer; // Referencia al LineRenderer.
    public float speed = 5f; // Velocidad del robot.
    public float rotationSpeed = 5f; // Velocidad de rotaci�n (suavidad).

    private int currentPointIndex = 0;
    private List<Vector3> linePoints = new List<Vector3>(); // Usamos una lista para actualizaci�n din�mica.

    public bool isContinuousMode = false; // Indica si el modo continuo est� activado.
    private bool isMoving = false; // Controla si el robot est� en movimiento.

    public void LoadLinePoints()
    {
        // Cargar los puntos actuales del LineRenderer.
        int pointCount = lineRenderer.positionCount;
        linePoints.Clear();

        for (int i = 0; i < pointCount; i++)
        {
            linePoints.Add(lineRenderer.GetPosition(i));
        }

        // Posicionar el robot en el primer punto de la l�nea.
        if (linePoints.Count > 0)
        {
            transform.position = linePoints[0];
            currentPointIndex = 0;
            isMoving = true;
        }
    }

    void Update()
    {
        if (linePoints == null || linePoints.Count == 0)
            return;

        if (isMoving)
        {
            MoveAlongLine();
        }

        // En modo continuo, revisa si hay nuevos puntos para agregar.
        if (isContinuousMode)
        {
            UpdateLinePoints();
        }
    }

    void MoveAlongLine()
    {
        // Moverse hacia el punto actual.
        Vector3 target = linePoints[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Calcular la direcci�n hacia el pr�ximo punto
        Vector3 direction = target - transform.position;

        // Rotar hacia la direcci�n del siguiente punto
        if (direction.magnitude > 0.1f)  // Evitar rotaci�n innecesaria cuando ya est� cerca
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Verificar si lleg� al punto.
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentPointIndex++;

            // Si lleg� al final de la l�nea.
            if (currentPointIndex >= linePoints.Count)
            {
                if (isContinuousMode)
                {
                    isMoving = false; // Esperar nuevos puntos.
                }
                else
                {
                    Debug.Log("El robot ha llegado al final de la l�nea.");
                    enabled = false; // Detener el movimiento en modo est�tico.
                }
            }
        }
    }

    void UpdateLinePoints()
    {
        // Agregar nuevos puntos al final de la lista.
        int pointCount = lineRenderer.positionCount;

        for (int i = linePoints.Count; i < pointCount; i++)
        {
            linePoints.Add(lineRenderer.GetPosition(i));
        }

        // Si hay nuevos puntos y el robot estaba detenido, reanudar el movimiento.
        if (!isMoving && linePoints.Count > currentPointIndex)
        {
            isMoving = true;
        }
    }
}
