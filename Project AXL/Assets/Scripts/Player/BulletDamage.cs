using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int damage; // Daño infligido por la bala
    public float torresEncontradas; // No está claro cómo se utiliza esta variable
    public bool isInmune = false; // Indica si la bala es inmune a ciertos objetos

    void Update()
    {
        // Comprobar si quedan torres en la escena
        HasTowersLeft();
    }

    // Comprobar si quedan torres en la escena
    void HasTowersLeft()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Torres");
        if (towers.Length > 0)
        {
            isInmune = true; // Si hay torres, la bala es inmune
            print("Inmune");
            print(towers.Length);
        }
        if (towers.Length <= 0)
        {
            isInmune = false; // Si no hay torres, la bala no es inmune
            print("No es inmune");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la bala colisiona con un objeto que tiene la etiqueta "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Infligir daño al enemigo y destruir la bala
            collision.gameObject.GetComponent<HealthController>().DamageCharacter(damage);
            Destroy(gameObject);
        }

        // Si la bala colisiona con un objeto que tiene la etiqueta "Torres"
        if (collision.gameObject.CompareTag("Torres"))
        {
            // Infligir daño a la torre y destruir la bala
            collision.gameObject.GetComponent<TowersHealthController>().DamageCharacter(damage);
            Destroy(gameObject);
        }

        // Si la bala colisiona con un objeto que tiene la etiqueta "Boss"
        if (collision.gameObject.CompareTag("Boss"))
        {
            // Si la bala es inmune, destruir la bala
            if (isInmune == true)
            {
                Destroy(gameObject);
                
            }
            else if (isInmune == false)
            {
                // Infligir daño al jefe y destruir la bala
                collision.gameObject.GetComponent<HealthController>().DamageCharacter(damage);
                Destroy(gameObject);
                
            }
        }
    }
}