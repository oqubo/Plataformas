using UnityEngine;

public class Player : MonoBehaviour, IAtaque
{

    private Rigidbody2D rb;
    private Animator anim;
    private float inputH;

    [SerializeField] private float vida;
    [SerializeField] private float danoAtaque;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float fuerzaSalto;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }


    void Update()
    {
        Movimiento();
        Saltar();
        Atacar();
        Pausa();

    }


    private void Movimiento()
    {
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

    public void Atacar()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetMouseButtonDown(0))
        {
            // si el jugador esta corriendo, no puede atacar
            if (anim.GetBool("corriendo"))
            {
                return;
            }
            
            // si el jugador esta saltando, no puede atacar
            if (Mathf.Abs(rb.linearVelocity.y) > 0.01f)
            {
                return;
            }
        
            anim.SetTrigger("atacar");
        }
    }



    private void Pausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CanvasManager.Instance.BtnPausar();

        }

    }

    public void RecibirDano(float danoRecibido)
    {
        vida -= danoRecibido;
        anim.SetTrigger("recibirDano");

        if (vida <= 0)
        {
            anim.SetTrigger("morir");

            // tengo que llamar a esto desde el final de la animacion de muerte
            GameManager.Instance.FinalizarPartida();
        }
    }  

}
