using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private float inputH;

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
    
    private void Atacar()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetTrigger("atacar");
        }
    }
    
}
