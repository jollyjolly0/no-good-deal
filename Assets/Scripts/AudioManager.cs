using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip bell;
    public AudioClip gameOver;
    public AudioClip win;
    public AudioClip hover;
    public AudioClip submit;
    public AudioClip questAccepted;
    public AudioClip questDenied;
    public AudioClip questComplete;

    public AudioSource sfxSource;

    public static AudioManager Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void PlayOneShot(AudioClip clip, float volume) {
        sfxSource.PlayOneShot(clip, volume);
    }
}
