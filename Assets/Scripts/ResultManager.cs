using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [Header("Componentes UI")]
    public TMP_Text resultText;  // Texto donde se mostrarán los resultados
    public GameObject resultPanel;  // Panel donde se muestra el texto de resultados

    [Header("Parámetros del Robot")]
    public RobotFollower robotFollower;  // Referencia al RobotFollower para obtener velocidad y peso
    public LineDrawer lineDrawer;        // Referencia al LineDrawer para obtener el tamaño de la línea

    private float startTime;
    private float endTime;
    private bool isRunning;

    void Start()
    {
        resultText.text = "";  // Inicializa el texto vacío
        isRunning = false;
        resultPanel.SetActive(false);  // Asegúrate de que el panel no esté visible al inicio
    }

    void Update()
    {
        // Empezamos el tiempo cuando el robot empieza a moverse
        if (!isRunning && robotFollower.isMoving)
        {
            startTime = Time.time;
            isRunning = true;
        }

        // Si el robot ha llegado al final, se captura el tiempo final y se muestra el resultado
        if (isRunning && !robotFollower.isMoving)
        {
            endTime = Time.time;
            DisplayResults();
            isRunning = false;
        }
    }

    // Método para calcular el tiempo total y mostrar los resultados
    void DisplayResults()
    {
        float totalTime = endTime - startTime;
        float lineWidth = lineDrawer.lineWidth;

        // Formateo del resultado
        string result = $"Prueba: {UIManager.trialCount}\n" +
                        "-------------------------------------\n" +
                        $"Velocidad: {robotFollower.speed:F2} Km/h\n" +
                        "-------------------------------------\n" +
                        $"Peso: {robotFollower.weight:F2} Kg\n" +
                        "-------------------------------------\n" +
                        $"Tamaño de la línea: {lineWidth:F2} m\n" +
                        "-------------------------------------\n" +
                        $"Tiempo total: {totalTime:F2} seg";

        // Mostrar el resultado en el texto de UI
        resultText.text = result;

        // Mostrar el panel con los resultados
        resultPanel.SetActive(true);
    }

    // Método para reiniciar los resultados
    public void ResetResults()
    {
        resultText.text = "";  // Limpiar los resultados
        isRunning = false;

        // Ocultar el panel después de reiniciar
        resultPanel.SetActive(false);
    }

    // Método para ocultar el panel al presionar el botón Ready
    public void HideResultPanel()
    {
        resultPanel.SetActive(false);
    }
}
