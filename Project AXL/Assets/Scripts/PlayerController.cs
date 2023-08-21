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
    public static int collectedBatteryAmount; // Número de cantidad de baterías recolectadas

    // Variables del disparo
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>(); // Permite acceder al componente Rigidbody2D a través de la variable rigidBody
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");

        if((shootHor != 0 || shootVert != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHor, shootVert);
            lastFire = Time.time;
        }

        rigidBody.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed); // Permite que el personaje se mueva en todas las direcciones
        collectedBatteryText.text = "Baterías recolectadas: " + collectedBatteryAmount;
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
}
