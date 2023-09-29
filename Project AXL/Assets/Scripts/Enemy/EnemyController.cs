using System.Collections;
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
    const string STATE_DIE = "Die";

    public GameObject batteryToInstance;

    [SerializeField] private AnimationClip dieAnimation;

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
        Die();
        life = GetComponent<HealthController>().currentHealth;
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
            StartCoroutine(DieAnim());
            
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