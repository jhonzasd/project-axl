using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ControlDeBoton : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Referencia al componente Video Player del primer video
    public VideoClip segundoVideo; // Video que se reproducirá al hacer clic en el botón
    public Button boton; // Referencia al componente Button del botón
    public string nombreDeLaNuevaEscena; // Nombre de la nueva escena a cargar

    void Start()
    {
        // Configura el evento de clic en el botón
        boton.onClick.AddListener(ReproducirSegundoVideo);

        // Suscribe el método al evento de finalización del primer video
        videoPlayer.loopPointReached += PrimerVideoTerminado;
    }

    void PrimerVideoTerminado(VideoPlayer vp)
    {
        // Este método se llama cuando el primer video ha terminado de reproducirse
        // Activa el botón para que el jugador pueda hacer clic en él
        boton.interactable = true;
    }

    void ReproducirSegundoVideo()
    {
        // Este método se llama cuando se hace clic en el botón
        // Desactiva el botón para evitar clics repetidos
        boton.interactable = false;

        // Cambia el video que se reproduce en el componente Video Player
        videoPlayer.clip = segundoVideo;
        videoPlayer.loopPointReached -= PrimerVideoTerminado; // Desuscribe el método anterior
        videoPlayer.loopPointReached += SegundoVideoTerminado; // Suscribe el método para el segundo video
        videoPlayer.Play();
    }

    void SegundoVideoTerminado(VideoPlayer vp)
    {
        // Este método se llama cuando el segundo video ha terminado de reproducirse
        // Carga la nueva escena aquí
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Cambia "nombreDeLaNuevaEscena" por el nombre de la escena que deseas cargar
    }
}