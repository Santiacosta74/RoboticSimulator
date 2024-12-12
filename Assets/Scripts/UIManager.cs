using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Para gestionar la recarga de la escena.

public class UIManager : MonoBehaviour
{
    public Button readyButton;
    public Button resetButton; // Nuevo botón Reset.
    public Slider speedSlider; // Slider para ajustar la velocidad.
    public Toggle lineToggle;  // Toggle para mostrar/ocultar la línea.
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
    }

    // Método para controlar la visibilidad de la línea desde el toggle.
    void ToggleLineVisibility(bool isVisible)
    {
        lineDrawer.enabled = isVisible;
    }
}
