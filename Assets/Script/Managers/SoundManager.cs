using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource globalEffectsSource; // Fuente de sonido central
    public AudioSource musicSource;        // Música de fondo

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Reproducir un efecto de sonido en una posición específica
    public void PlaySoundAtPosition(AudioClip clip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(clip, position); // Reproduce el sonido en la posición dada
    }

    // Reproducir un efecto de sonido global (sin posición específica)
    public void PlayGlobalSound(AudioClip clip)
    {
        globalEffectsSource.PlayOneShot(clip);
    }
}
