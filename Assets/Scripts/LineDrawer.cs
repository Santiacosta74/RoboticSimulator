using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  // Agregado para poder utilizar el EventSystem

public class LineDrawer : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer lineRenderer;
    public float lineWidth = 0.05f; // Ancho inicial de la línea

    private List<Vector3> points = new List<Vector3>();

    void Start()
    {
        if (!lineRenderer)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 0;  // Asegúrate de que no haya puntos al inicio
    }

    void Update()
    {
        // Dibuja la línea solo si el botón izquierdo del ratón está presionado y no está sobre la UI
        if (Input.GetMouseButton(0) && !IsPointerOverUI())
        {
            DrawLine();
        }
    }

    void DrawLine()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 point = hit.point;

            if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], point) > 0.1f)
            {
                points.Add(point);
                lineRenderer.positionCount = points.Count;
                lineRenderer.SetPosition(points.Count - 1, point);
            }
        }
    }

    // Función para actualizar el ancho de la línea en el LineRenderer
    public void UpdateLineWidth()
    {
        // Actualizamos tanto el ancho de inicio como el de fin
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        // Forzamos la actualización visual de la línea
        lineRenderer.GetComponent<LineRenderer>().enabled = false;
        lineRenderer.GetComponent<LineRenderer>().enabled = true;
    }

    // Función para verificar si el puntero está sobre algún elemento de la UI
    bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
