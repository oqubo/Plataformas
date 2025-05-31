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




    public override void Atacar()
    {
        anim.SetTrigger("atacar");
    }

  
    
}
