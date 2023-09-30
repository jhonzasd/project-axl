using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CambioDeEscenaAlFinalizarVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Referencia al componente Video Player

    void Start()
    {
        // Suscribe el método al evento de finalización del video
        videoPlayer.loopPointReached += VideoTerminado;
    }

    void VideoTerminado(VideoPlayer vp)
    {
        // Este método se llama cuando el video ha terminado de reproducirse
        // Carga la nueva escena aquí
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Cambia "NuevaEscena" por el nombre de la escena que deseas cargar
    }
}