using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;//velocidad de movimiento
    [SerializeField] private Transform Player;//deteccion de player

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);//moverse a la posicion de player
    }
}