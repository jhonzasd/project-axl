using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed;//velocidad del disparo
    Rigidbody2D rb;
    private Transform player;//detectar player


    void Start()
    {
        player = FindAnyObjectByType<PlayerController>().transform;
        rb = GetComponent<Rigidbody2D>();
        disparar();
    }

    void disparar()//salir disparado en la direccion del enemigo
    {
        Vector2 haciaElJugador = (player.position - transform.position).normalized;
        rb.velocity = haciaElJugador * speed;
        StartCoroutine(drestruir());
    }
    void OnCollisionEnter2D()//destruirse al chocar con algo
    {
        Destroy(gameObject);
    }

    IEnumerator drestruir()//destruirse tras pasar un tiempo
    {
        float tiempoduso = 5f;//tiempo necesesario para que el disparo desaparesca
        yield return new WaitForSeconds(tiempoduso);
        Destroy(gameObject);
    }
}
