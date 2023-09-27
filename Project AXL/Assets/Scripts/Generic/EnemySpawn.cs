using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawn : MonoBehaviour
{
    private float minx, maxX, miny, maxy;

    [SerializeField] private Transform[] puntos;//puntos de spawn
    [SerializeField] private GameObject[] enemigos;//carpeta de los enemigos 

    [SerializeField] private float tiempospawn;//tiempo entre enemigo y enemigo

    private float tiempoSiguienteEnemigo;

    void Start()
    {
        //definicion para los puntos de spawn
        maxX = puntos.Max(puntos => puntos.position.x);
        minx = puntos.Min(puntos => puntos.position.x);
        maxy = puntos.Max(puntos => puntos.position.y);
        miny = puntos.Min(puntos => puntos.position.y);
    }

    void Update()
    {
        //lineas de codigo para que espere antes de spawnear otro enemigo
        tiempoSiguienteEnemigo += Time.deltaTime;

        if (tiempoSiguienteEnemigo >= tiempospawn)
        {
            tiempoSiguienteEnemigo = 0;
            crearenemigo();
        }
    }
    void crearenemigo()//linea para que elija un enemigo alasar en la carpeta de enemigos 
    {
        int numeroenemigo = Random.Range(0, enemigos.Length);
        Vector2 posicionalt = new Vector2(Random.Range(minx, maxX), Random.Range(miny, maxy));

        Instantiate(enemigos[numeroenemigo], posicionalt, Quaternion.identity);
    }
}