using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followTarget; // Variable que hace referencia al objeto que va a seguir la cámara
    [SerializeField]
    private Vector3 targetPosition; // Variable que hace referencia a la posición del objeto
    [SerializeField]
    private float cameraSpeed = 5.0f; // Velocidad de la cámara (Tiene que ser igual a la del jugador)
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = new Vector3(followTarget.transform.position.x, 
                                    followTarget.transform.position.y, 
                                    this.transform.position.z);

        this.transform.position = Vector3.Lerp(this.transform.position,
                                               targetPosition, 
                                               cameraSpeed * Time.deltaTime);
    }
}
