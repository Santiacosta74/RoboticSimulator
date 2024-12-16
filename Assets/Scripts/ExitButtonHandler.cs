using UnityEngine;
using UnityEngine.UI;  // Aseg�rate de tener este namespace para poder trabajar con UI

public class ExitButtonHandler : MonoBehaviour
{
    public Button exitButton;

    void Start()
    {
        // Nos aseguramos de que el bot�n est� asignado y le a�adimos el listener
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnExitButtonClicked);
        }
    }

    // M�todo que se llama cuando el bot�n de salida es presionado
    void OnExitButtonClicked()
    {
        // Si estamos en el editor de Unity, mostramos un mensaje (es �til para pruebas)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
