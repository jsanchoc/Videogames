using UnityEngine;

public class NinjaFrogBehaviour : MonoBehaviour
{
    [SerializeField] private int numVidas = 3;
    [SerializeField] private int velocidad = 5;
    [SerializeField] AudioSource audioSource;

    private Rigidbody2D rigidbody;
    private Animator anim;
    private bool estaSuelo;
    private SpriteRenderer spriteRenderer;
    // Referencia para el HUDController
    private HUDController hudController; 
    public AudioClip jump;
    public Transform FirePoint;
    public GameObject Arrow;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource.clip = jump;

        // **INTEGRACIÓN 1: Buscar y conectar el HUDController**
        hudController = FindObjectOfType<HUDController>();

        // **INTEGRACIÓN 2: Inicializar el HUD al inicio del juego**
        if (hudController != null)
        {
            hudController.UpdateNinjaFrogLivesDisplay(numVidas);
        }
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            gameObject.transform.position += new Vector3(-1, 0, 0) * velocidad * Time.deltaTime;
            anim.SetBool("isRunning", true);
            
        }
        // ... (resto del código de movimiento) ...
        else if(Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.transform.position += new Vector3(1, 0, 0) * velocidad * Time.deltaTime;
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if(Input.GetKeyDown(KeyCode.W) && estaSuelo)
        {
            rigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
            estaSuelo = false;
            audioSource.Play();
        }
        else
        {
            anim.SetBool("isJumping", false);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(Arrow, FirePoint.position, transform.rotation);
        }
        if (numVidas <= 0)
        {
            anim.SetTrigger("isHit");
            Destroy(gameObject, 0.5f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        estaSuelo = true;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 normal = collision.contacts[0].normal;
            if (normal.y > 0.5f)
            {
                Destroy(collision.gameObject);
                return;
            }
            else
            {
                anim.SetTrigger("isHit");
                numVidas--;
                Debug.Log(numVidas);
                
                // **INTEGRACIÓN 3: Llamada al HUD para actualizar las vidas**
                if (hudController != null)
                {
                    hudController.UpdateNinjaFrogLivesDisplay(numVidas);
                }
                return;
            }
        }

        if (collision.gameObject.CompareTag("Spikes"))
        {
            anim.SetTrigger("isHit");
            numVidas--;
            Debug.Log(numVidas);
            
            // **INTEGRACIÓN 4: Llamada al HUD para actualizar las vidas**
            if (hudController != null)
            {
                hudController.UpdateNinjaFrogLivesDisplay(numVidas);
            }
        }
    }
}