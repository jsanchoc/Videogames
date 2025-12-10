using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // ← Agrega

public class FinishFlag : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;  // ← Arrastra Canvas aquí
    private int playersHit = 0;
    private const int REQUIRED_PLAYERS = 2;
    private bool levelComplete = false;  // ← Evita múltiples triggers

    void OnTriggerEnter2D(Collider2D col)
    {
        if (levelComplete) return;  // ← Protección

        if (col.CompareTag("GreenPlayer") || col.CompareTag("PinkPlayer"))
        {
            playersHit++;
            if (playersHit >= REQUIRED_PLAYERS)
            {
                LevelComplete();
            }
        }
    }

    void LevelComplete()
    {
        levelComplete = true;
        Time.timeScale = 0f;  // ← PARA TODO EL JUEGO [web:42]
        victoryPanel.SetActive(true);  // ← MUESTRA "LEVEL COMPLETED!"
        Debug.Log("¡LEVEL COMPLETED!");
    }
}
