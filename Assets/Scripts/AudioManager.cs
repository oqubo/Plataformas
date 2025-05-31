using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; private set; }

    public AudioSource musicSource;
    public AudioSource sfxSource;

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


    public void ReproducirMusica(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void ReproducirSonido(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

}
