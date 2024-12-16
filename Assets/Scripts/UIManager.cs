using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

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
    public TMP_Text weightText;
    public TMP_InputField weightInputField;
    public Toggle lineToggle;
    public Slider lineWidthSlider;  // Slider para controlar el ancho de la línea

    [Header("Componentes del Robot")]
    public LineDrawer lineDrawer;
    public RobotFollower robotFollower;

    [Header("Parámetros del Robot")]
    public float baseWeight = 50f;        // Peso base del robot (Kg).

    private void Start()
    {
        readyButton.onClick.AddListener(OnReadyClicked);
        resetButton.onClick.AddListener(OnResetClicked);

        speedSlider.onValueChanged.AddListener(UpdateSpeedFromSlider);
        speedSlider.value = robotFollower.baseSpeed;

        speedInputField.onEndEdit.AddListener(UpdateSpeedFromInput);

        weightSlider.onValueChanged.AddListener(UpdateWeightFromSlider);
        weightSlider.value = baseWeight;

        weightInputField.onEndEdit.AddListener(UpdateWeightFromInput);

        lineToggle.onValueChanged.AddListener(ToggleLineVisibility);
        lineToggle.isOn = lineDrawer.enabled;

        lineWidthSlider.onValueChanged.AddListener(UpdateLineWidthFromSlider);  // Escuchar cambios del slider de ancho de línea
        lineWidthSlider.value = lineDrawer.lineWidth;  // Iniciar el slider con el valor actual del ancho de la línea

        UpdateSpeedText(speedSlider.value);
        UpdateWeightText(baseWeight);
    }

    void OnReadyClicked()
    {
        lineDrawer.enabled = false;
        robotFollower.LoadLinePoints();
        Debug.Log("Dibujo finalizado. Robot preparado para moverse.");
    }

    void OnResetClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

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
            UpdateWeightFromSpeed(newSpeed);
        }
        else
        {
            Debug.LogWarning("Entrada inválida para velocidad.");
        }
    }

    private void UpdateWeightFromSpeed(float newSpeed)
    {
        throw new NotImplementedException();
    }

    void UpdateWeightFromSlider(float value)
    {
        UpdateWeight(value);
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
        UpdateSpeedTextPosition();
    }

    void UpdateWeightText(float baseWeight)
    {
        weightText.text = $"{baseWeight:F2} Kg";
        UpdateSpeedFromWeight();
    }

    void UpdateWeight(float weight)
    {
        weightText.text = $"{weight:F2} Kg";
        UpdateSpeedFromWeight();
    }

    void ToggleLineVisibility(bool isVisible)
    {
        lineDrawer.enabled = isVisible;
    }

    void UpdateSpeedTextPosition()
    {
        if (speedSlider.fillRect != null)
        {
            RectTransform handleRect = speedSlider.handleRect;
            Vector3 handlePosition = handleRect.position;
            speedTextRect.position = handlePosition + new Vector3(0, 30f, 0);
        }
    }

    void UpdateLineWidthFromSlider(float value)
    {
        lineDrawer.lineWidth = value;  // Actualiza el ancho de la línea en el LineDrawer
    }
}
