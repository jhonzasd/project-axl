using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    public float speed;//velocidad del disparo
    Rigidbody2D rb;
    private Transform player;//detectar player

    public float lifeTime;
    public int damage = 25;

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>().transform;
        rb = GetComponent<Rigidbody2D>();
        disparar();
        StartCoroutine(DeathDelay());
    }

    void disparar()//salir disparado en la direccion del enemigo
    {
        Vector2 haciaElJugador = (player.position - transform.position).normalized;
        rb.velocity = haciaElJugador * speed;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<HealthController>().DamageCharacter(damage);
            Destroy(gameObject);
        }
    }

    IEnumerator DeathDelay()//destruirse tras pasar un tiempo
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
