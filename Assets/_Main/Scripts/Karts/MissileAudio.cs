using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    private bool isPlaying;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        isPlaying = false;
    }

    private void Update()
    {
        if (enabled)
        {
            if (isPlaying) return;
            // Spawn audio at location
            AudioSource.PlayClipAtPoint(_audioSource.clip, transform.position);
            isPlaying = true;
        }
    }
}
