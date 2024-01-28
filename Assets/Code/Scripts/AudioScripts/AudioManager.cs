using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    [Header("------------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource laugthSource;
    [SerializeField] AudioSource ambientSource;
    [SerializeField] AudioSource SFXSource;



    [Header("------------- Audio clip ----------")]
    public AudioClip backGround;
    public AudioClip[] musicClips;
    public AudioClip[] fxClips;
    public AudioClip[] laughtClips;

    public float minTimeBetweenSounds = 5f;  // Tiempo mínimo entre sonidos
    public float maxTimeBetweenSounds = 15f; // Tiempo máximo entre sonidos

    private float nextSoundTime;
    // Tiempo en el que se reproducirá el próximo sonido

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        GameManager.onLaughter += PlayLaughtSound;
    }

    private void OnDestroy()
    {
        GameManager.onLaughter -= PlayLaughtSound;
    }

    private void Start()
    {
        musicSource.clip = backGround;
        musicSource.Play();

        SetNextSoundTime();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    void Update()
    {
        // Verificar si es el momento de reproducir un sonido
        if (Time.time >= nextSoundTime)
        {
            // Elegir aleatoriamente entre reproducir música o sonido
            if (Random.Range(0, 2) == 0)
            {
                // Reproducir música aleatoria
                PlayRandomMusic(musicClips);
            }
            else
            {
                // Reproducir sonido aleatorio
                PlayRandomFX(fxClips);
            }

            // Establecer el próximo tiempo de reproducción del sonido
            SetNextSoundTime();
        }

    }

    void PlayRandomMusic(AudioClip[] clips)
    {
        // Elegir aleatoriamente un clip de la lista
        AudioClip randomClip = clips[Random.Range(0, clips.Length)];

        // Reproducir el clip
        musicSource.PlayOneShot(randomClip);
    }

    void PlayRandomFX(AudioClip[] clips)
    {
        // Elegir aleatoriamente un clip de la lista
        AudioClip randomClip = clips[Random.Range(0, clips.Length)];
        // Reproducir el clip
        laugthSource.PlayOneShot(randomClip);
    }

    void PlayLaughtSound()
    {
        float value = GameManager.Instance.laugherValue;

        // Elegir aleatoriamente un clip de la lista
        AudioClip randomClip = laughtClips[(int)Map(value, 0, 100, 0, laughtClips.Length)];

        // Reproducir el clip
        SFXSource.PlayOneShot(randomClip);
    }

    void SetNextSoundTime()
    {
        // Calcular el próximo tiempo de reproducción del sonido en un intervalo aleatorio
        nextSoundTime = Time.time + Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds);
    }


    float Map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
