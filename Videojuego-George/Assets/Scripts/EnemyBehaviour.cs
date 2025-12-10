using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float patrolDistance = 5f;
    public float velocidad = 2f;

    private float startX; // posición inicial en X
    private float yOriginal; // mantén Y fijo
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        startX = transform.position.x;
        yOriginal = transform.position.y;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.PingPong(Time.time * velocidad, patrolDistance) + (startX - patrolDistance / 2);
        // Posición actual antes de cambiar
        float previousX = transform.position.x;
        transform.position = new Vector2(x, transform.position.y);

        // Flip invirtiendo escala X
        if (x > previousX)
            transform.localScale = new Vector3(0.6f, 0.6f, 1f); // Mira derecha
        else if (x < previousX)
            transform.localScale = new Vector3(-0.6f, 0.6f, 1f); // Mira izquierda
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("arrow"))
        {
            Destroy(gameObject);
        }
    }

}
