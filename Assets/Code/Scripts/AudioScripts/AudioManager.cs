using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource ambientSource;
    [SerializeField] AudioSource SFXSource;


    [Header("------------- Audio clip ----------")]
    public AudioClip backGround;
    public AudioClip[] musicClips;
    public AudioClip[] fxClips;

    public float minTimeBetweenSounds = 5f;  // Tiempo mínimo entre sonidos
    public float maxTimeBetweenSounds = 15f; // Tiempo máximo entre sonidos

    private float nextSoundTime;  // Tiempo en el que se reproducirá el próximo sonido


    private void Start()
    {
        ambientSource.clip = backGround;
        ambientSource.Play();

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
        SFXSource.PlayOneShot(randomClip);
    }

    void SetNextSoundTime()
    {
        // Calcular el próximo tiempo de reproducción del sonido en un intervalo aleatorio
        nextSoundTime = Time.time + Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds);
    }
}
