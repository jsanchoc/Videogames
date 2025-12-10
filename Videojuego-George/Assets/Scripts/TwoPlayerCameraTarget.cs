using UnityEngine;
using Cinemachine;

public class TwoPlayerCameraTarget : MonoBehaviour
{
    [Header("Jugadores")]
    public Transform player1;
    public Transform player2;
    
    [Header("Configuración")]
    public float followThresholdX = 5f;
    public CinemachineVirtualCamera vcam;
    public float fixedY = 0f;
    
    private bool isFollowing = false;
    private Transform centerTarget;

    void Start()
    {
        if (player1 == null || player2 == null) 
        {
            Debug.LogError("¡ASIGNA player1 y player2!");
            return;
        }
        
        GameObject centerObj = new GameObject("PlayersCenter");
        centerObj.transform.SetParent(transform);
        centerTarget = centerObj.transform;
        centerTarget.position = new Vector3(0, fixedY, -10);
        
        Debug.Log("✅ Script iniciado. Centro creado.");
    }

    void Update()
    {
        if (player1 == null || player2 == null || vcam == null) 
        {
            Debug.LogError("¡FALTAN REFERENCIAS!");
            return;
        }
        
        float centerX = (player1.position.x + player2.position.x) * 0.5f;
        Debug.Log($"CentroX: {centerX:F1} | Umbral: {followThresholdX} | Siguiendo: {isFollowing}");
        
        if (!isFollowing && centerX >= followThresholdX)
        {
            vcam.Follow = centerTarget;
            isFollowing = true;
            Debug.Log("🎥 CÁMARA EMPIEZA A SEGUIR");
        }
        else if (isFollowing && centerX < followThresholdX)
        {
            vcam.Follow = null;
            isFollowing = false;
            Debug.Log("🛑 CÁMARA PARA DE SEGUIR");
        }
    }

    void LateUpdate()
    {
        if (centerTarget != null)
        {
            float centerX = (player1.position.x + player2.position.x) * 0.5f;
            centerTarget.position = new Vector3(centerX, fixedY, centerTarget.position.z);
        }
    }
}
