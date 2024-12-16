using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [Header("Componentes UI")]
    public TMP_Text resultText;  // Texto donde se mostrar�n los resultados
    public GameObject resultPanel;  // Panel donde se muestra el texto de resultados

    [Header("Par�metros del Robot")]
    public RobotFollower robotFollower;  // Referencia al RobotFollower para obtener velocidad y peso
    public LineDrawer lineDrawer;        // Referencia al LineDrawer para obtener el tama�o de la l�nea

    private float startTime;
    private float endTime;
    private bool isRunning;

    void Start()
    {
        resultText.text = "";  // Inicializa el texto vac�o
        isRunning = false;
        resultPanel.SetActive(false);  // Aseg�rate de que el panel no est� visible al inicio
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

    // M�todo para calcular el tiempo total y mostrar los resultados
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
                        $"Tama�o de la l�nea: {lineWidth:F2} m\n" +
                        "-------------------------------------\n" +
                        $"Tiempo total: {totalTime:F2} seg";

        // Mostrar el resultado en el texto de UI
        resultText.text = result;

        // Mostrar el panel con los resultados
        resultPanel.SetActive(true);
    }

    // M�todo para reiniciar los resultados
    public void ResetResults()
    {
        resultText.text = "";  // Limpiar los resultados
        isRunning = false;

        // Ocultar el panel despu�s de reiniciar
        resultPanel.SetActive(false);
    }

    // M�todo para ocultar el panel al presionar el bot�n Ready
    public void HideResultPanel()
    {
        resultPanel.SetActive(false);
    }
}
