using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int routine;
    private Vector2 movement;
    private float chronometer;
    private float vertical;
    private float horizontal;
    public float speed; // Velocidad de movimiento
    Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); // Permite acceder al componente Rigidbody2D a través de la variable rigidBody
    }

    void Update()
    {
        chronometer += Time.deltaTime; // Al cronómetro se le suma el tiempo transcurrido
        if (chronometer >= 4) // Cada 4 segundos
        {
            routine = Random.Range(1, 3);
            chronometer = 0; // Reinicia el cronómetro
        }
        switch (routine)
        {
            case 1:
                horizontal = Random.Range(0, 5);
                vertical = Random.Range(0, 5);
                movement = new Vector2(horizontal, vertical);
                routine++;
                break;

            case 2:
                rigidBody.MovePosition(rigidBody.position + movement * speed * Time.deltaTime);
                break;
        }
    }
}
