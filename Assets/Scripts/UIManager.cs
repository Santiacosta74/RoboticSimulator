using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Componentes UI")]
    public Button readyButton;
    public Button resetButton;
    public Slider speedSlider;
    public TMP_Text speedText;
    public RectTransform speedTextRect;
    public TMP_InputField speedInputField;
    public Slider weightSlider;
    public TMP_InputField weightInputField;
    public TMP_Text weightText;
    public TMP_Text resultText;
    public static int trialCount = 0;

    [Header("Componentes del Robot")]
    public LineDrawer lineDrawer;
    public RobotFollower robotFollower;

    [Header("Parámetros del Robot")]
    public float baseWeight = 50f;
    public Slider lineWidthSlider;

    private ResultManager resultManager;

    private void Start()
    {
        resultManager = FindObjectOfType<ResultManager>();  // Obtener la referencia al ResultManager

        readyButton.onClick.AddListener(OnReadyClicked);
        resetButton.onClick.AddListener(OnResetClicked);

        speedSlider.onValueChanged.AddListener(UpdateSpeedFromSlider);
        speedSlider.value = robotFollower.baseSpeed;

        speedInputField.onEndEdit.AddListener(UpdateSpeedFromInput);

        weightSlider.onValueChanged.AddListener(UpdateWeightFromSlider);
        weightSlider.value = baseWeight;

        weightInputField.onEndEdit.AddListener(UpdateWeightFromInput);

        lineWidthSlider.onValueChanged.AddListener(UpdateLineWidthFromSlider);  // Conectar el slider de ancho de línea

        UpdateSpeedText(speedSlider.value);
        UpdateWeightText(baseWeight);
        UpdateResultText();  // Muestra el contador de pruebas desde 0
    }

    void OnReadyClicked()
    {
        lineDrawer.enabled = false;
        robotFollower.LoadLinePoints();
        Debug.Log("Dibujo finalizado. Robot preparado para moverse.");

        // Incrementar el número de prueba
        trialCount++;
        UpdateResultText();

        // Ocultar el panel de resultados
        resultManager.HideResultPanel();
    }

    void OnResetClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        UpdateResultText();
    }

    void UpdateResultText()
    {
        resultText.text = $"";  // Limpiar los resultados al reiniciar
    }

    // Métodos para actualizar la velocidad, peso, etc.
    void UpdateSpeedFromSlider(float value)
    {
        robotFollower.baseSpeed = value;
        UpdateSpeedFromWeight();
    }

    void UpdateSpeedFromInput(string value)
    {
        if (float.TryParse(value, out float newSpeed))
        {
            newSpeed = Mathf.Clamp(newSpeed, speedSlider.minValue, speedSlider.maxValue);
            robotFollower.baseSpeed = newSpeed;
            speedSlider.value = newSpeed;
            UpdateSpeedFromWeight();
        }
        else
        {
            Debug.LogWarning("Entrada inválida para velocidad.");
        }
    }

    void UpdateWeightFromSlider(float value)
    {
        UpdateWeight(value);
        UpdateWeightText(value);
    }

    void UpdateWeightFromInput(string value)
    {
        if (float.TryParse(value, out float newWeight))
        {
            newWeight = Mathf.Clamp(newWeight, weightSlider.minValue, weightSlider.maxValue);
            weightSlider.value = newWeight;
            UpdateWeight(newWeight);
        }
        else
        {
            Debug.LogWarning("Entrada inválida para peso.");
        }
    }

    void UpdateSpeedFromWeight()
    {
        robotFollower.weight = weightSlider.value;
        robotFollower.UpdateSpeedBasedOnWeight();
        UpdateSpeedText(robotFollower.speed);
    }

    void UpdateSpeedText(float value)
    {
        speedText.text = $"{value:F2} Km/h";
    }

    void UpdateWeightText(float baseWeight)
    {
        weightText.text = $"{baseWeight:F2} Kg";
    }

    void UpdateWeight(float weight)
    {
        weightText.text = $"{weight:F2} Kg";
        UpdateSpeedFromWeight();
    }

    // Función para actualizar el ancho de la línea
    void UpdateLineWidthFromSlider(float value)
    {
        lineDrawer.lineWidth = value;  // Cambiar el ancho de la línea
        lineDrawer.UpdateLineWidth();  // Actualizar visualmente el ancho de la línea
    }
}
