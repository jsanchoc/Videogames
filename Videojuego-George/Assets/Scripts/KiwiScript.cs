using UnityEngine;

public class KiwiScript : MonoBehaviour
{
    [SerializeField] private int kiwisCollected = 0;
    [SerializeField] AudioSource audioSource;
    public AudioClip collected;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GreenPlayer")
        {
            anim.SetTrigger("isCollected");
            Destroy(gameObject, 1f);
            audioSource.clip = collected;
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}