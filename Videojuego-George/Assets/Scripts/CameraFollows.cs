using UnityEngine;

public class CameraFollows : MonoBehaviour {
    public Transform player1;
    public Transform player2;
    public float speed = 5f;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float startX = 5f;
    public float stopX = 50f;
    
    private bool isFollowing = false;
    private float fixedY;
    private float fixedZ;

    void Start() {
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate() {
        if (player1 == null || player2 == null) return;
        
        float midX = (player1.position.x + player2.position.x) / 2f;
        
        // Inicia si pasa startX
        if (!isFollowing && midX >= startX) {
            isFollowing = true;
        }
        // Para si pasa stopX
        if (isFollowing && midX >= stopX) {
            isFollowing = false;
        }
        
        if (isFollowing) {
            float targetX = midX + offset.x;
            Vector3 targetPos = new Vector3(targetX, fixedY, fixedZ);
            transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
        }
    }
}
