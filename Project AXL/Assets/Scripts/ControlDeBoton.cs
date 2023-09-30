using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ControlDeBoton : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Referencia al componente Video Player del primer video
    public VideoClip segundoVideo; // Video que se reproducir� al hacer clic en el bot�n
    public Button boton; // Referencia al componente Button del bot�n
    public string nombreDeLaNuevaEscena; // Nombre de la nueva escena a cargar

    void Start()
    {
        // Configura el evento de clic en el bot�n
        boton.onClick.AddListener(ReproducirSegundoVideo);

        // Suscribe el m�todo al evento de finalizaci�n del primer video
        videoPlayer.loopPointReached += PrimerVideoTerminado;
    }

    void PrimerVideoTerminado(VideoPlayer vp)
    {
        // Este m�todo se llama cuando el primer video ha terminado de reproducirse
        // Activa el bot�n para que el jugador pueda hacer clic en �l
        boton.interactable = true;
    }

    void ReproducirSegundoVideo()
    {
        // Este m�todo se llama cuando se hace clic en el bot�n
        // Desactiva el bot�n para evitar clics repetidos
        boton.interactable = false;

        // Cambia el video que se reproduce en el componente Video Player
        videoPlayer.clip = segundoVideo;
        videoPlayer.loopPointReached -= PrimerVideoTerminado; // Desuscribe el m�todo anterior
        videoPlayer.loopPointReached += SegundoVideoTerminado; // Suscribe el m�todo para el segundo video
        videoPlayer.Play();
    }

    void SegundoVideoTerminado(VideoPlayer vp)
    {
        // Este m�todo se llama cuando el segundo video ha terminado de reproducirse
        // Carga la nueva escena aqu�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Cambia "nombreDeLaNuevaEscena" por el nombre de la escena que deseas cargar
    }
}