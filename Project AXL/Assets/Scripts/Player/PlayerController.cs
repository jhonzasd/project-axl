using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Variables de movimiento del personaje
    public float movementSpeed; // Velocidad de movimiento
    Rigidbody2D rigidBody;

    // Variables de recolección
    public TMP_Text collectedBatteryText; // Texto de cantidad de baterías recolectadas
    public static int collectedBatteryAmount = 9; // Número de cantidad de baterías recolectadas

    // Variables del disparo
    public GameObject bulletPrefab;
    public float bulletSpeed; // Velocidad de la bala
    private float lastFire; // Tiempo que ha transcurrido desde el último disparo
    public float fireDelay; // Tiempo de delay de la bala

    // Animación
    Animator animator;
    private bool walking = false;
    public Vector2 lastMovement = Vector2.zero;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";

    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";
    private const string walkingState = "Walking";

    // Batería
    [SerializeField] private float battery;
    [SerializeField] private float maxBattery;
    [SerializeField] BatteryBarManager batteryBar;

    


    // Sprite
    SpriteRenderer sprite;
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>(); // Permite acceder al componente Rigidbody2D a través de la variable rigidBody
        animator = GetComponent<Animator>(); // Permite acceder al componente Animator a través de la variable animator
        sprite = GetComponent<SpriteRenderer>();

        if (animator == null)
        {
            Debug.LogError("Animator is not assigned!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        walking = false;
     
        if(Mathf.Abs(Input.GetAxisRaw(horizontal)) > 0.5f)
        {
            this.transform.Translate(
                new Vector3(Input.GetAxisRaw(horizontal) * movementSpeed * Time.deltaTime, 0, 0));
            walking = true;
            lastMovement = new Vector2(Input.GetAxisRaw(horizontal), 0);
        }

        if(Mathf.Abs(Input.GetAxisRaw(vertical)) > 0.5f)
        {
            this.transform.Translate(
                new Vector3(0, Input.GetAxisRaw(vertical) * movementSpeed * Time.deltaTime, 0));
            walking = true;
            lastMovement = new Vector2(0, Input.GetAxisRaw(vertical));
        }

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");

        if((shootHor != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHor, 0);
            lastFire = Time.time;

        }
        if ((shootVert != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(0, shootVert);
            lastFire = Time.time;
        }
        
        collectedBatteryText.text = "Baterías recolectadas: " + collectedBatteryAmount;

        // Animación
        Animation();
        
    
    }

    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed
            );                                                                                                                                                    
    }

    void Animation()            
    {
       animator.SetFloat(horizontal, Input.GetAxisRaw(horizontal));
       animator.SetFloat(vertical, Input.GetAxisRaw(vertical));

       animator.SetBool(walkingState, walking);

        animator.SetFloat(lastHorizontal, lastMovement.x);
        animator.SetFloat(lastVertical, lastMovement.y);

       
       sprite.flipX = lastMovement.x < -0.5f || Input.GetAxisRaw(horizontal) < -0.5f;
    }
}
