using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform[] towerSpawnPoints; // Puntos de spawn de las torres

    public float shootInterval; // Tasa de espera para disparar
    public float minDistance; // Distancia mínima entre el enemigo y el jugador
    public float speed; // Velocidad de movimiento
    public float life = 300f;
    public float secondPhase = 250f; // Vida necesaria para cambiar de fase

    [SerializeField] private Transform player; // Detección del jugador
    [SerializeField] private Transform point0; // Punto de regreso en la fase 2
    [SerializeField] private GameObject towerPrefab; // Punto donde aparecen las torres

    private float foundTowers;
    private float timeShot;
    private List<Transform> unusedPoints = new List<Transform>();
    Animator animator;
    [SerializeField] private AnimationClip deathAnimation;

    void Start()
    {
        // Agregar todos los puntos de spawn a la lista de puntos no utilizados al inicio
        unusedPoints.AddRange(towerSpawnPoints);

        animator = GetComponent<Animator>();
        player = FindAnyObjectByType<PlayerController>().transform;
    }

    void Update()
    {
        timeShot += Time.deltaTime;
        GameObject[] foundTowers = GameObject.FindGameObjectsWithTag("Torres");
        int foundTowersCount = foundTowers.Length;

        life = GetComponent<HealthController>().currentHealth; // Obtener la vida actual del jefe

        if (life <= secondPhase) // Cambiar a la segunda fase
        {
            foreach (Transform TowerSpawnPoint in unusedPoints)
            {
                Instantiate(towerPrefab, TowerSpawnPoint.position, Quaternion.identity);
            }

            // Limpiar la lista de puntos de spawn, ya que todos han sido utilizados
            unusedPoints.Clear();

            transform.position = Vector3.MoveTowards(transform.position, point0.position, speed * Time.deltaTime); // Moverse hacia la posición 0

            if (timeShot >= shootInterval)
            {
                timeShot = 0;
                StartCoroutine(Shoot()); // Disparar
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime); // Moverse hacia la posición del jugador
        }
    }

    IEnumerator Shoot() // Código para disparar
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
    }

    public void Die()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    /*IEnumerator DieAnim()
    {
        animator.SetTrigger("Die");

        yield return new WaitForSeconds(deathAnimation.length);

        Destroy(gameObject);
    }*/
}