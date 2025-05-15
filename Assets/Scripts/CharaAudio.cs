using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaAudio : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] footsteps;
    public float pitchOffset;

    private void Start()
    {
        //InvokeRepeating(nameof(CharaStep), 0.1f, 1f);   
    }

    public void CharaStep()
    {
        AudioClip soundToPlay = footsteps[Random.Range(0, footsteps.Length)];
        source.Stop();
        source.clip = soundToPlay;
        source.pitch = (1 + Random.Range(-pitchOffset,pitchOffset));
        source.Play();
    }
}
