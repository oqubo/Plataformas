using System.Collections;
using UnityEngine;

public class MagoQueGolpea : Enemigo
{


    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Patrulla());
    }

    // Update is called once per frame
    void Update()
    {


    }




    public override void LanzarAtaque()
    {
        anim.SetTrigger("atacar");
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

        // sonido
        AudioManager.Instance.ReproducirSonido(sonidoAtaque);


    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(puntoAtaque.position, radioAtaque);
    }
    
  
    
}
