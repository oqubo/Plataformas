using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Elimina duplicados
        }

        // Configuracion de FPS
#if UNITY_EDITOR // Limitar FPS en el editor
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
#else // Sin limite en compilado
        Application.targetFrameRate = -1;
#endif

    }



    public void CargarEscena(int sceneBuildIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneBuildIndex);
    }

    public void ReiniciarEscena()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        CargarEscena(currentSceneIndex);
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
#if UNITY_EDITOR // Detiene el juego en el editor
        UnityEditor.EditorApplication.isPlaying = false;
#else // Cierra la aplicacion en compilado
        Application.Quit(); 
#endif
    }


    /*
        public void GuardarPartida()
        {
            if (PlayerPrefs.GetInt("nivelDesbloqueado") < nivel)
                PlayerPrefs.SetInt("nivelDesbloqueado", nivel);
            if (PlayerPrefs.GetInt("puntosMax") < puntos)
                PlayerPrefs.SetInt("puntosMax", puntos);
        }

        public void cargarPartida(){
            nivel = PlayerPrefs.GetInt("nivelDesbloqueado");
            puntos = PlayerPrefs.GetInt("puntosMax");
            }
    */


}
