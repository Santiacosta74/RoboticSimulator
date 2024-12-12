using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Importar para detectar la UI.

public class LineDrawer : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer lineRenderer;
    public float lineWidth = 0.05f;
    public LayerMask drawMask;

    private List<Vector3> points = new List<Vector3>();

    void Start()
    {
        if (!lineRenderer)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Botón izquierdo del mouse presionado.
        {
            // Verifica si el mouse está sobre la UI.
            if (IsPointerOverUI())
                return;

            DrawLine();
        }
    }

    void DrawLine()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, drawMask))
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

    bool IsPointerOverUI()
    {
        // Devuelve true si el mouse está sobre un elemento de UI.
        return EventSystem.current.IsPointerOverGameObject();
    }
}
