using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;// Para gestionar la recarga de la escena.

public class UIManager : MonoBehaviour
{
    [Header("Componentes UI")]
    public Button readyButton;
    public Button resetButton; // Nuevo botón Reset.
    public Slider speedSlider; // Slider para ajustar la velocidad.
    public TMP_Text speedText;     // Texto para mostrar la velocidad encima del Slider.
    public RectTransform speedTextRect; // RectTransform del texto para posicionarlo dinámicamente.
    public Toggle lineToggle;  // Toggle para mostrar/ocultar la línea.

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
        lineToggle.isOn = lineDrawer.enabled;  // Ajustar el toggle según si la línea está activa o no.

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

    // Método para actualizar la velocidad del robot desde el slider.
    void UpdateSpeed(float value)
    {
        robotFollower.speed = value;

        // Actualizar el texto con el valor de la velocidad
        speedText.text = $"{value:F2}Km/h";

        // Mover el texto para que esté sobre el handle del slider
        UpdateSpeedTextPosition();
    }

    // Método para controlar la visibilidad de la línea desde el toggle.
    void ToggleLineVisibility(bool isVisible)
    {
        lineDrawer.enabled = isVisible;
    }

    // Método para mover el texto encima del handle del slider
    void UpdateSpeedTextPosition()
    {
        if (speedSlider.fillRect != null)
        {
            // Obtener la posición del handle
            RectTransform handleRect = speedSlider.handleRect;
            Vector3 handlePosition = handleRect.position;

            // Mover el texto a la posición del handle
            speedTextRect.position = handlePosition + new Vector3(0, 30f, 0); // Ajustar el desplazamiento vertical (30f) según lo necesario.
        }
    }
}
