using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Murcielago : Enemigo
{

    public Transform jugador;

    private Coroutine patrullaRoutine;
    private Coroutine persecucionRoutine;
    private bool persiguiendo = false;


    void Start()
    {
        anim = GetComponent<Animator>();
        patrullaRoutine = StartCoroutine(Patrulla());
    }

    // Update is called once per frame
    void Update()
    {



    }


    public override void LanzarAtaque()
    {
        persiguiendo = true;
        // Detener patrulla antes de perseguir
        if (patrullaRoutine != null)
        {
            StopCoroutine(patrullaRoutine);
            patrullaRoutine = null;
        }

        persecucionRoutine = StartCoroutine(PerseguirJugador());


    }

    public override void Atacar()
    {

        Collider2D[] colisiones = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque, queEsDanable);

        foreach (Collider2D col in colisiones)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<Player>().RecibirDano(danoAtaque);
            }
        }

    }

    IEnumerator PerseguirJugador()
    {
        while (true)
        {
            Vector3 destino = jugador.position;
            float distancia = Vector3.Distance(transform.position, destino);
            float duracion = distancia / (velocidadMovimiento * 2);

            transform.DOMove(destino, duracion).SetEase(Ease.Linear);
            anim.SetTrigger("atacar");
            yield return new WaitForSeconds(duracion);

            if (distancia < 2f)
            {
                Atacar();
                break;
            }
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(puntoAtaque.position, radioAtaque);
    }

}
