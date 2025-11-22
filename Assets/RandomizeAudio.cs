using System.Collections.Generic;
using UnityEngine;

public class RandomizeAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> audioClips;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (audioSource == null)
        {
            audioSource = this.gameObject.GetComponent<AudioSource>();
        }
    }

    public void RandomizeAudioClip()
    {
        if (audioClips != null)
        {
            if (audioClips.Count > 0)
            {
                int randomIndex = Random.Range(0, audioClips.Count);

                audioSource.clip = audioClips[randomIndex];
            }
        }
    }

    public void PlayRandomClip()
    {
        RandomizeAudioClip();
        audioSource.Play();
    }
}
