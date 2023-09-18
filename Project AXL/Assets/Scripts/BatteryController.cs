using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatteryController : MonoBehaviour
{

    // Batería
    [SerializeField] private float maxBattery;
    [SerializeField] public BatteryBarManager batteryBar;
    [SerializeField] private float battery = 1.0f;
    void Start()
    {
        batteryBar = FindAnyObjectByType<BatteryBarManager>();
        batteryBar.SumarBateria(0);
    }

    // Update is called once per frame
    void Update()
    { 

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            batteryBar.SumarBateria(battery);
            PlayerController.collectedBatteryAmount++;
            Destroy(gameObject);
        }
    }
}
