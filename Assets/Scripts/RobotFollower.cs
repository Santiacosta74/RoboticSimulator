using System.Collections.Generic;
using UnityEngine;

public class RobotFollower : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float baseSpeed;   // Velocidad base antes de ajustes (esto es lo que ahora se calcula desde el peso).
    public float weight;      // Peso del robot.

    private int currentPointIndex = 0;
    private List<Vector3> linePoints = new List<Vector3>();
    public bool isMoving = false;

    [Header("Configuración de Velocidades")]
    public float minSpeed = 5f; // Velocidad mínima en km/h
    public float maxSpeed = 70f; // Velocidad máxima en km/h
    public float minWeight = 10f; // Peso mínimo en kg
    public float maxWeight = 100f; // Peso máximo en kg

    public void LoadLinePoints()
    {
        int pointCount = lineRenderer.positionCount;
        linePoints.Clear();

        for (int i = 0; i < pointCount; i++)
        {
            linePoints.Add(lineRenderer.GetPosition(i));
        }

        if (linePoints.Count > 0)
        {
            transform.position = linePoints[0];
            currentPointIndex = 0;
            isMoving = true;
        }
    }

    void Update()
    {
        if (linePoints == null || linePoints.Count == 0 || !isMoving)
            return;

        MoveAlongLine();
    }

    void MoveAlongLine()
    {
        Vector3 target = linePoints[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        Vector3 direction = target - transform.position;
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentPointIndex++;
            if (currentPointIndex >= linePoints.Count)
            {
                isMoving = false;
                Debug.Log("El robot ha llegado al final de la línea.");
            }
        }
    }

    public void UpdateSpeedBasedOnWeight()
    {
        // Interpolar la velocidad base basada en el peso
        float calculatedSpeed = Mathf.Lerp(maxSpeed, minSpeed, (weight - minWeight) / (maxWeight - minWeight));
        baseSpeed = Mathf.Clamp(calculatedSpeed, minSpeed, maxSpeed);
        speed = baseSpeed;  // La velocidad final se ajusta con la baseSpeed calculada
    }
}
