using System;
using UnityEngine;

public class MetaController : MonoBehaviour
{
    [SerializeField] private AudioSource metaSound;
    private bool MetaAlcanzada = false;

    void Awake()
    {
        if (metaSound != null) metaSound = GetComponent<AudioSource>();
        metaSound.playOnAwake = false;
        metaSound.loop = false;
        metaSound.Stop();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (MetaAlcanzada == false)
            {
                Debug.Log("¡Has llegado a la " + gameObject.name + "!");
                metaSound.Play();
            }
            
            if (other.TryGetComponent<PlayerController>(out var player))
                player.SetSpawn(gameObject.transform);
            MetaAlcanzada = true;
        }
    }
}
