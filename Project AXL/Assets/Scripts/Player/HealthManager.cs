using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int health = 300; // Establece la cantidad de vida del jugador

    public Image[] hearts; // Crea un array (lista) con los sprites de los corazones
    public Sprite heart0; // Crea una variable que almacena el sprite del corazón lleno
    public Sprite heart25; // Crea una variable que almacena el sprite del corazón vacío
    public Sprite heart50;
    public Sprite heart75;
    public Sprite heart100;

    private void FillHeart(Image heart, int fill)
    {
        switch (fill)
        {
            case 100:
                heart.sprite = heart100;
                break;
            case 75:
                heart.sprite = heart75;
                break;
            case 50:
                heart.sprite = heart50;
                break;
            case 25:
                heart.sprite = heart25;
                break;
            default:
                heart.sprite = heart0;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Todos los corazones inician vacíos
        foreach (Image heart in hearts)
        {
            FillHeart(heart, 0);
        }

        // Determinamos cuantos corazones se llenan enteros
        int hearts_parts = health / 25;
        int exact_hearts = (int)(hearts_parts / 4);

        // Corazón que no se llena completamente
        int remaining = hearts_parts % 4;

        // Se llenan los corazones completos
        for (int i = 0; i < exact_hearts; i++)
        {
            FillHeart(hearts[i], 4 * 25);
        }

        // Si hay un corazón que se llena parcialmente se realiza la acción
        if (remaining > 0)
        {
            FillHeart(hearts[exact_hearts], remaining * 25);
        }        
    }
}
