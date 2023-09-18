using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryBarManager : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void CambiarBateriaMaxima(float bateriaMaxima)
    {
        slider.maxValue = bateriaMaxima;
    }

    public void SumarBateria(float cantidadBateria)
    {
        slider.value += cantidadBateria;
    }

    public void InicializarBarraBateria(float cantidadBateria)
    {
        CambiarBateriaMaxima(cantidadBateria);
        SumarBateria(cantidadBateria);
    }
}
