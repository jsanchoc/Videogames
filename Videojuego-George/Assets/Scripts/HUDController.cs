using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour 
{
    // Referencias del Jugador 1 (Pink Man)
    public TextMeshProUGUI pinkLivesText; 

    // **NUEVA REFERENCIA para el Jugador 2 (Ninja Frog)**
    public TextMeshProUGUI ninjaFrogLivesText; 

    public static HUDController Instance; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Función de actualización del J1 (ya la tenías)
    public void UpdatePinkLivesDisplay(int newLivesCount)
    {
        string newText = string.Format("X {0}", newLivesCount);
        pinkLivesText.text = newText;
    }

    // **NUEVA FUNCIÓN para el Jugador 2**
    public void UpdateNinjaFrogLivesDisplay(int newLivesCount)
    {
        string newText = string.Format("X {0}", newLivesCount);
        ninjaFrogLivesText.text = newText;
    }
}