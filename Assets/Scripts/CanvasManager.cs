using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }
    // UI
    [Header("UI")]
    [SerializeField] private GameObject popupMenu;
    [SerializeField] private TextMeshProUGUI textoPuntos;
    [SerializeField] private UnityEngine.UI.Image barraVida;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        popupMenu.SetActive(false);
    }



    public void BtnPausar()
    {
        // mostrar menu de pausa
        popupMenu.SetActive(true);
        Debug.Log("Juego pausado");
        GameManager.Instance.PausarJuegoOn();
    }

    public void BtnContinuar()
    {
        // quitar menu de pausa
        popupMenu.SetActive(false);
        Debug.Log("Juego reanudado");
        GameManager.Instance.PausarJuegoOff();
    }
    public void BtnReiniciar()
    {
        GameManager.Instance.ReiniciarEscena();
    }
    public void BtnSalir()
    {
        GameManager.Instance.SalirDeJuego();
    }



    public void UIactualizarPuntos(int puntos)
    {
        textoPuntos.text = puntos.ToString();
    }

    public void UIactualizarVida(float vida)
    {
        barraVida.fillAmount = vida;
    }

}
