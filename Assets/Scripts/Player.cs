using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private float inputH;

    [SerializeField] private float velocidadMovimiento;
    public float VelocidadMovimiento
    {
        get { return velocidadMovimiento; }
        set { velocidadMovimiento = value; }
    }
    
    [SerializeField] private float fuerzaSalto;
    public float FuerzaSalto
    {
        get { return fuerzaSalto; }
        set { fuerzaSalto = value; }
    }




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
           
    }

    // Update is called once per frame
    void Update()
    {
        inputH = Input.GetAxis("Horizontal");

        // Move the player
        rb.linearVelocity = new Vector2(inputH * velocidadMovimiento, rb.linearVelocity.y);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }

        // Flip the player based on direction
        if (inputH > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (inputH < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
    }
}
