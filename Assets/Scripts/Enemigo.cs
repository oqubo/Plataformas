using System.Collections;
using UnityEngine;

public abstract class Enemigo : MonoBehaviour, IAtaque
{
    [SerializeField] protected float vida;
    [SerializeField] protected float nivel;

    [Header("Ataque")]
    [SerializeField] protected float danoAtaque;
    [SerializeField] protected Transform puntoAtaque;
    [SerializeField] protected float radioAtaque;
    [SerializeField] protected LayerMask queEsDanable;


    [Header("Patrulla")]
    [SerializeField] protected Transform[] puntosPatrulla;
    [SerializeField] protected float velocidadMovimiento;
    protected Vector3 destinoActual;
    protected int indiceActual = 0;

    [Header("Sonidos")]
    [SerializeField] protected AudioClip sonidoAtaque;
    [SerializeField] protected AudioClip sonidoMorir;

    protected Animator anim;

    protected Tweens tweens;

    protected virtual void Awake()
    {
        tweens = GetComponent<Tweens>();
        if (tweens == null)
        {
            tweens = gameObject.AddComponent<Tweens>();
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    protected IEnumerator Patrulla()
    {
        destinoActual = puntosPatrulla[indiceActual].position; 

        while (true)
        {
            while (transform.position != destinoActual)
            {
                transform.position = Vector2.MoveTowards(transform.position, destinoActual,
                    velocidadMovimiento * Time.deltaTime);
                yield return null;
            }
            DefinirNuevoDestino();
        }
    }

    protected void DefinirNuevoDestino()
    {
        indiceActual++;
        if (indiceActual >= puntosPatrulla.Length)
        {
            indiceActual = 0;
        }
        destinoActual = puntosPatrulla[indiceActual].position;

        // enfocar destino
        if (destinoActual.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        // si lo detecta (colider de deteccion)
        if (other.CompareTag("PlayerDeteccion"))
        {
            LanzarAtaque();
        }

        // si lo traspasa (colider interior)
        // se hacen los dos un daño igual al daño de ataque
        else if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().RecibirDano(danoAtaque);
            RecibirDano(danoAtaque);
        }
    }
        

    public abstract void LanzarAtaque();
    public abstract void Atacar();
    

    public void RecibirDano(float danoRecibido)
    {
        vida -= danoAtaque;
        tweens.Parpadeo();

        if (vida <= 0)
        {
            anim.SetTrigger("morir");
            Destroy(gameObject, 2f);
        }
    }
    
}
