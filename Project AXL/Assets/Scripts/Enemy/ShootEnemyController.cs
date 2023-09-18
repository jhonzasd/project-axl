using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemyController : MonoBehaviour
{
    [SerializeField] private GameObject proyectilPrefab;
    public float tiempodisp;//tasa de espera para disparar
    public float distanciamin;// float para la distancia entre enemigo y player minima
    public float speed;//velocidad del enemigo
    [SerializeField] private Transform Player;// deteccion del player

    void Start()
    {
        StartCoroutine(Disparo());
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, Player.position) > distanciamin)//si se esta mas lejos de player que la distancia minima acercarse
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);

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
}