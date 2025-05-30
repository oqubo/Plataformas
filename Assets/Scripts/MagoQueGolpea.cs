using System.Collections;
using UnityEngine;

public class MagoQueGolpea : MonoBehaviour
{
    [SerializeField] private Transform[] puntosPatrulla;
    [SerializeField] private float velocidadMovimiento;
    private Vector3 destinoActual;
    private int indiceActual = 0;

    void Start()
    {
        destinoActual = puntosPatrulla[indiceActual].position;
        StartCoroutine(Patrulla());
    }

    // Update is called once per frame
    void Update()
    {


    }

    IEnumerator Patrulla()
    {
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

    private void DefinirNuevoDestino()
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
        if (other.CompareTag("Player"))
        {
            // Aquí puedes agregar la lógica para golpear al jugador
            Debug.Log("¡El mago ha golpeado al jugador!");
        }
    }


}
