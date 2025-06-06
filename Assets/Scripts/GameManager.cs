using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

//------------------------------
// CREACION
//------------------------------
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Elimina duplicados
            Destroy(gameObject);
        }

        ConfigurarFPS();
    }


    private void ConfigurarFPS()
    {
        // Limitar FPS en el editor
#if UNITY_EDITOR
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        // Sin limite en compilado
#else
        Application.targetFrameRate = -1;
#endif
    }


//------------------------------
// ESCENAS
//------------------------------

    public void CargarEscena(int sceneBuildIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneBuildIndex);
    }

    public void ReiniciarEscena()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        CargarEscena(currentSceneIndex);
        PausarJuegoOff();
    }


    public void PausarJuegoOn()
    {
        Time.timeScale = 0;
    }

    public void PausarJuegoOff()
    {
        Time.timeScale = 1;
    }

    public void SalirDeJuego()
    {
        // Detiene el juego en el editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        // Cierra la aplicacion en compilado
#else
        Application.Quit(); 
#endif

        // guardar partida
        GuardarPartida();
    }


    public void FinalizarPartida()
    {
        Invoke("ReiniciarEscena", 5f);
    }


//------------------------------
// GUARDADO
//------------------------------

    public void GuardarPartida()
    {
        /*
        if (PlayerPrefs.GetInt("nivelDesbloqueado") < nivel)
            PlayerPrefs.SetInt("nivelDesbloqueado", nivel);
        if (PlayerPrefs.GetInt("puntosMax") < puntos)
            PlayerPrefs.SetInt("puntosMax", puntos);
        */
    }

    public void CargarPartida()
    {
        /*
        nivel = PlayerPrefs.GetInt("nivelDesbloqueado");
        puntos = PlayerPrefs.GetInt("puntosMax");
        */
    }



}
