using System;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{

    [SerializeField] private EnumDireccion direccion;
    [SerializeField] private float distancia;
    [SerializeField] private float velocidad;

    private enum EnumDireccion { Vertical, Horizontal}

    protected Tweens tweens;

    protected virtual void Awake()
    {
        tweens = GetComponent<Tweens>();
        if (tweens == null)
        {
            tweens = gameObject.AddComponent<Tweens>();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (direccion == EnumDireccion.Horizontal)
        {
            tweens.MoverIzquierdaDerecha(distancia, velocidad);
        }
        else if (direccion == EnumDireccion.Vertical)
        {
            tweens.MoverArribaAbajo(distancia, velocidad);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
