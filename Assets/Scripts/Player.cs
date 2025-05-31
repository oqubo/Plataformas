using UnityEngine;

public class Player : MonoBehaviour, IAtaque
{

    private Rigidbody2D rb;
    private Animator anim;
    private float inputH;

    [SerializeField] private float vida;
    public float Vida
    {
        get { return vida; }
        set
        {
            vida = value;
            CanvasManager.Instance.UIactualizarVida(vida);
        }
    }

    [SerializeField] private int puntos;

    public int Puntos
    {
        get { return puntos; }
        set
        {
            puntos = value;
            CanvasManager.Instance.UIactualizarPuntos(puntos);
        }
    }

    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float fuerzaSalto;

    [Header("Ataque")]
    [SerializeField] private float danoAtaque;
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private LayerMask queEsDanable;

    [Header("Efectos")]
    [SerializeField] private GameObject efectoDano;
    [SerializeField] private GameObject efectoMoneda;

    [Header("Sonidos")]
    [SerializeField] private AudioClip sonidoAtaque;
    [SerializeField] private AudioClip sonidoRecibirDano;
    [SerializeField] private AudioClip sonidoMorir;
    [SerializeField] private AudioClip sonidoSaltar;
    [SerializeField] private AudioClip sonidoPuntos;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


        Vida = 100f;
        Puntos = 0;
    }


    void Update()
    {
        Movimiento();
        Saltar();
        LanzarAtaque();
        Pausa();

    }


    private void Movimiento()
    {

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        // Evita movimiento en estados de ataque o muerte
        if (stateInfo.IsTag("NoMover"))
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }


        inputH = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(inputH * velocidadMovimiento, rb.linearVelocity.y);
        if (inputH != 0)
        {
            anim.SetBool("corriendo", true);
            // mirar a la derecha
            if (inputH > 0) transform.eulerAngles = Vector3.zero;
            // mirar a la izquierda
            else transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            anim.SetBool("corriendo", false);
        }


    }

    private void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            anim.SetTrigger("saltar");
        }
    }

    public void LanzarAtaque()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("atacar");
        }
    }




    public void Atacar()
    {
        Collider2D[] colisiones = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque, queEsDanable);
        foreach (Collider2D col in colisiones)
        {
            if (col.CompareTag("Enemy"))
            {
                col.GetComponent<Enemigo>().RecibirDano(danoAtaque);
            }
        }

        // sonido
        AudioManager.Instance.ReproducirSonido(sonidoAtaque);
    }

    public void RecibirDano(float danoRecibido)
    {
        // valor
        Vida -= danoRecibido;

        // animacion
        anim.SetTrigger("recibirDano");

        // efecto
        GameObject instancia = Instantiate(efectoDano, transform.position, Quaternion.identity);
        Destroy(instancia, 1f);

        // sonido
        AudioManager.Instance.ReproducirSonido(sonidoRecibirDano);


        if (vida <= 0)
        {
            anim.SetTrigger("morir");

            // tengo que llamar a esto desde el final de la animacion de muerte
            GameManager.Instance.FinalizarPartida();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(puntoAtaque.position, radioAtaque);
    }


    private void Pausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CanvasManager.Instance.BtnPausar();

        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Moneda"))
        {
            // sumar puntos
            Puntos++;

            // efecto
            GameObject instancia = Instantiate(efectoMoneda, transform.position, Quaternion.identity);
        Destroy(instancia, 0.5f);

            // sonido
            AudioManager.Instance.ReproducirSonido(sonidoPuntos);

            // destruir moneda
            Destroy(collision.gameObject);
        }
    }

}
