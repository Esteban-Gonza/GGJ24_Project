using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public AudioClip[] footstepsClip;
    public AudioClip[] breathingClip;

    public AudioSource footstepsAudioSource;
    public AudioSource breathingAudioSource;

    public float minTimeBetweenSounds = 1f;  // Tiempo mínimo entre sonidos
    public float maxTimeBetweenSounds = 10f; // Tiempo máximo entre sonidos

    private float nextSoundTime;

    private void Start()
    {
        SetNextSoundTime();
    }

    // Lógica para reproducir sonidos en diferentes situaciones
    void Update()
    {
        if (Time.time >= nextSoundTime)
        {
            PlayBreathing();
            SetNextSoundTime();
        }

    }

    public void PlayFootsteps()
    {
        AudioClip randomClip = footstepsClip[Random.Range(0, footstepsClip.Length)];
        footstepsAudioSource.PlayOneShot(randomClip);
    }

    void PlayBreathing()
    {
        if (!breathingAudioSource.isPlaying)
        {
            AudioClip randomClip = breathingClip[Random.Range(0, breathingClip.Length)];
            breathingAudioSource.PlayOneShot(randomClip);
        }
    }

    void SetNextSoundTime()
    {
        // Calcular el próximo tiempo de reproducción del sonido en un intervalo aleatorio
        nextSoundTime = Time.time + Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds);
    }
}
