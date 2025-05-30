using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class CicloDiaNoche : MonoBehaviour
{
    private Light2D luzAmbiental;

    [SerializeField] private Color colorDia = new Color(1.0f, 0.6f, 0.3f);
    [SerializeField] private Color colorNoche = new Color(0.1f, 0.1f, 0.4f);
    [SerializeField] private float duracionDia = 30f;
    [SerializeField] private float duracionNoche = 30f;
    [SerializeField] private float duracionTransicion = 5f;

    void Start()
    {
        luzAmbiental = GetComponent<Light2D>();
        StartCoroutine(CicloDiaNocheCoroutine());
    }

    IEnumerator CicloDiaNocheCoroutine()
    {
        while (true)
        {
            // Día
            luzAmbiental.color = colorDia;
            yield return new WaitForSeconds(duracionDia);

            // Transición a noche
            yield return StartCoroutine(CambiarColor(colorDia, colorNoche, duracionTransicion));

            // Noche
            luzAmbiental.color = colorNoche;
            yield return new WaitForSeconds(duracionNoche);

            // Transición a día
            yield return StartCoroutine(CambiarColor(colorNoche, colorDia, duracionTransicion));
        }
    }

    IEnumerator CambiarColor(Color origen, Color destino, float duracion)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duracion;
            luzAmbiental.color = Color.Lerp(origen, destino, t);
            yield return null;
        }
        luzAmbiental.color = destino;
    }
}