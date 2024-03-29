using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    public float speed = 2.0f;
    public Transform player;
    public float life = 300f;

    SpriteRenderer sprite;

    const string STATE_UP = "isMovingUp";
    const string STATE_DOWN = "isMovingDown";
    const string STATE_HORIZONTAL = "isMovingHorizontal";

    private void Start()
    {
        animator = GetComponent<Animator>();
        // Utiliza FindObjectOfType para encontrar el objeto PlayerController
        player = FindAnyObjectByType<PlayerController>().transform;
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        Animation();
    }

    void Animation()
    {
        Vector3 direction = player.position - transform.position;
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
            // TODO: Reproducir animación de morir
            Destroy(gameObject);
        }
    }
}