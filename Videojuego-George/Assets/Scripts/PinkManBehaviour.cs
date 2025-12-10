using UnityEngine;

public class PinkManBehaviour : MonoBehaviour
{
    [SerializeField] private int numVidas = 3;
    [SerializeField] private int velocidad = 5;
    [SerializeField] AudioSource audioSource;

    private Rigidbody2D rigidbody;
    private Animator anim;
    private bool estaSuelo;
    private SpriteRenderer spriteRenderer;
    // Referencia para comunicarnos con la interfaz de usuario
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

        // **PASO CLAVE 1:** Encontrar el script HUDController en la escena (estará en el Canvas)
        hudController = FindObjectOfType<HUDController>(); 
        
        // **PASO CLAVE 2:** Inicializar el display del HUD con las vidas actuales al inicio del juego.
        if (hudController != null)
        {
            hudController.UpdatePinkLivesDisplay(numVidas);
        }
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            gameObject.transform.position += new Vector3(-1, 0, 0) * velocidad * Time.deltaTime;
            anim.SetBool("isRunning", true);
            
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.transform.position += new Vector3(1, 0, 0) * velocidad * Time.deltaTime;
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && estaSuelo)
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Arrow, FirePoint.position, transform.rotation);
        }

        if (numVidas <= 0)
        {
            anim.SetTrigger("isHit");
            // Destruimos el objeto después de un pequeño retraso para que se vea la animación
            Destroy(gameObject, 0.5f); 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        estaSuelo = true;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 normal = collision.contacts[0].normal;
            
            // Lógica de Salto (Si lo pisa por arriba, el enemigo muere, el jugador no recibe daño)
            if (normal.y > 0.5f)
            {
                Destroy(collision.gameObject);
                return;
            }
            // Lógica de DAÑO (Si choca de lado o por abajo, el jugador pierde vida)
            else
            {
                anim.SetTrigger("isHit");
                numVidas--;
                Debug.Log(numVidas);

                // **PASO CLAVE 3:** Llama al HUD para que muestre el nuevo número de vidas
                if (hudController != null)
                {
                    hudController.UpdatePinkLivesDisplay(numVidas);
                }
                return;
            }
        }

        if (collision.gameObject.CompareTag("Spikes"))
        {
            anim.SetTrigger("isHit");
            numVidas--;
            Debug.Log(numVidas);
            
            // **PASO CLAVE 4:** Llama al HUD para que muestre el nuevo número de vidas
            if (hudController != null)
            {
                hudController.UpdatePinkLivesDisplay(numVidas);
            }
        }
    }
}