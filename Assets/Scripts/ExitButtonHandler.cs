using UnityEngine;
using UnityEngine.UI;  // Asegúrate de tener este namespace para poder trabajar con UI

public class ExitButtonHandler : MonoBehaviour
{
    public Button exitButton;

    void Start()
    {
        // Nos aseguramos de que el botón esté asignado y le añadimos el listener
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnExitButtonClicked);
        }
    }

    // Método que se llama cuando el botón de salida es presionado
    void OnExitButtonClicked()
    {
        // Si estamos en el editor de Unity, mostramos un mensaje (es útil para pruebas)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
