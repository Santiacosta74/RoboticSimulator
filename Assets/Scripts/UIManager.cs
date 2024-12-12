using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;// Para gestionar la recarga de la escena.

public class UIManager : MonoBehaviour
{
    [Header("Componentes UI")]
    public Button readyButton;
    public Button resetButton; // Nuevo bot�n Reset.
    public Slider speedSlider; // Slider para ajustar la velocidad.
    public TMP_Text speedText;     // Texto para mostrar la velocidad encima del Slider.
    public RectTransform speedTextRect; // RectTransform del texto para posicionarlo din�micamente.
    public Toggle lineToggle;  // Toggle para mostrar/ocultar la l�nea.

    [Header("Componentes del Robot")]
    public LineDrawer lineDrawer;
    public RobotFollower robotFollower;

    private void Start()
    {
        // Vincular los botones y acciones
        readyButton.onClick.AddListener(OnReadyClicked);
        resetButton.onClick.AddListener(OnResetClicked);

        // Vincular el slider y el toggle
        speedSlider.onValueChanged.AddListener(UpdateSpeed);
        speedSlider.value = robotFollower.speed;  // Ajustar el slider al valor actual de la velocidad.

        lineToggle.onValueChanged.AddListener(ToggleLineVisibility);
        lineToggle.isOn = lineDrawer.enabled;  // Ajustar el toggle seg�n si la l�nea est� activa o no.

        // Actualizar el texto inicialmente
        UpdateSpeed(speedSlider.value);
    }

    void OnReadyClicked()
    {
        // Desactiva la funcionalidad de dibujo.
        lineDrawer.enabled = false;

        // Cargar los puntos en el robot.
        robotFollower.LoadLinePoints();

        Debug.Log("Dibujo finalizado. Robot preparado para moverse.");
    }

    void OnResetClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // M�todo para actualizar la velocidad del robot desde el slider.
    void UpdateSpeed(float value)
    {
        robotFollower.speed = value;

        // Actualizar el texto con el valor de la velocidad
        speedText.text = $"{value:F2}Km/h";

        // Mover el texto para que est� sobre el handle del slider
        UpdateSpeedTextPosition();
    }

    // M�todo para controlar la visibilidad de la l�nea desde el toggle.
    void ToggleLineVisibility(bool isVisible)
    {
        lineDrawer.enabled = isVisible;
    }

    // M�todo para mover el texto encima del handle del slider
    void UpdateSpeedTextPosition()
    {
        if (speedSlider.fillRect != null)
        {
            // Obtener la posici�n del handle
            RectTransform handleRect = speedSlider.handleRect;
            Vector3 handlePosition = handleRect.position;

            // Mover el texto a la posici�n del handle
            speedTextRect.position = handlePosition + new Vector3(0, 30f, 0); // Ajustar el desplazamiento vertical (30f) seg�n lo necesario.
        }
    }
}
