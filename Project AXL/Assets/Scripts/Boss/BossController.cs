using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class bosscontroller : MonoBehaviour
{
    [SerializeField] private GameObject proyectilPrefab;
    [SerializeField] private Transform[] puntospawntorre;//puntos de spawn de las torres
    public float tiempodisp;//tasa de espera para disparar
    public float distanciamin;// float para la distancia entre enemigo y player minima
    public float speed;//velocidad de movimiento
    public float life = 300f;
    public float segundafase = 250f;//vida necesaria para cambiar de fase
    [SerializeField] private Transform Player;//deteccion de player
    [SerializeField] private Transform punto0;//punto de regreso en fase 2
    [SerializeField] private Transform torresprefab;//punto donde las torres aparecen
    private float torresEncontradas;
    [SerializeField] private float tiempodispa;
    private float disparado;
    private List<Transform> puntosNoUtilizados = new List<Transform>();

    Animator animator;
    [SerializeField] private AnimationClip dieAnimation;
    void Start()
    {
        // Agregar todos los puntos de spawn a la lista de puntos no utilizados al inicio
        puntosNoUtilizados.AddRange(puntospawntorre);

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        disparado += Time.deltaTime;
        GameObject[] torresEncontradas = GameObject.FindGameObjectsWithTag("torres");
        int torresEncontradasn = torresEncontradas.Length;


        life = GetComponent<HealthController>().currentHealth;//lo que dijiste que pusiera
        if (life <= segundafase)//if para pasar a la segunda fase
        {
            foreach (Transform spawnPoint in puntosNoUtilizados)
            {
                Instantiate(torresprefab, spawnPoint.position, Quaternion.identity);
            }

            // Limpiar la lista de puntos de spawn, ya que todos han sido utilizados
            puntosNoUtilizados.Clear();

            transform.position = Vector3.MoveTowards(transform.position, punto0.position, speed * Time.deltaTime);//moverse a posicion 0

            if (disparado >= tiempodispa)
            {
                disparado = 0;
                StartCoroutine(Disparo());//disparar
            }


        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);//moverse a la posicion de player
        }
    }

    IEnumerator Disparo()//codigo para disparar
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempodisp);
            Instantiate(proyectilPrefab, transform.position, Quaternion.identity);
        }
    }

    public void Die()
    {
        if (life <= 0)
        {
            StartCoroutine(DieAnim());

        }

    }
    IEnumerator DieAnim()
    {

        animator.SetTrigger("Die");

        yield return new WaitForSeconds(dieAnimation.length);

        Destroy(gameObject);
    }
}