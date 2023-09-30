using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemyController : MonoBehaviour
{
    [SerializeField] private GameObject proyectilPrefab;
    public float tiempodisp;//tasa de espera para disparar
    public float distanciamin;// float para la distancia entre enemigo y player minima
    public float speed;//velocidad del enemigo
    [SerializeField] public Transform Player;// deteccion del player

    [SerializeField] private AnimationClip dieAnimation;

    public float life = 150f;
    public GameObject batteryToInstance;

    Animator animator;
    SpriteRenderer sprite;
    const string STATE_UP = "isMovingUp";
    const string STATE_DOWN = "isMovingDown";
    const string STATE_HORIZONTAL = "isMovingHorizontal";

    public SFXManager sfxManager;
    void Start()
    {
        StartCoroutine(Disparo());
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        Player = FindAnyObjectByType<PlayerController>().transform;
        sfxManager = FindFirstObjectByType<SFXManager>();
    }

    void Update()
    {
        Animation();
        Die();
        life = GetComponent<HealthController>().currentHealth;
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

    void Animation()
    {
        Vector3 direction = Player.position - transform.position;
        direction.Normalize();

        bool isUp = direction.y > 0;
        bool isDown = direction.y < 0;
        bool isRight = direction.x > 0;
        bool isLeft = direction.x < 0;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            animator.SetBool(STATE_HORIZONTAL, true);
            animator.SetBool(STATE_UP, false);
            animator.SetBool(STATE_DOWN, false);
        }
        else
        {
            animator.SetBool(STATE_HORIZONTAL, false);
            animator.SetBool(STATE_UP, isUp);
            animator.SetBool(STATE_DOWN, isDown);
        }

        sprite.flipX = isLeft;
    }

    public void Die()
    {
        if (life <= 0)
        {
            
            StartCoroutine(DieAnim());
            sfxManager.playerDead.Play();

        }

    }
    IEnumerator DieAnim()
    {

        animator.SetTrigger("Die");

        yield return new WaitForSeconds(dieAnimation.length);

        Destroy(gameObject);
        DropBattery();
    }

    public void DropBattery()
    {
        Vector3 thisPosition = transform.position;
        Quaternion thisRotation = Quaternion.identity;

        GameObject batteryInstance = Instantiate(batteryToInstance, thisPosition, thisRotation);
    }
}