using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity=transform.right * Speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
