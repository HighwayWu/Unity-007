using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip cursor;
    public AudioClip drop;
    public AudioClip control;
    public AudioClip lineClear;

    private Ctrl ctrl;
    private bool isMute = false;
    private AudioSource audioSource;

    void Awake()
    {
        Screen.SetResolution(576, 1024, false);
        ctrl = GetComponent<Ctrl>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCursor()
    {
        PlayAudio(cursor);
    }

    public void PlayDrop()
    {
        PlayAudio(drop);
    }

    public void PlayControl()
    {
        PlayAudio(control);
    }

    public void PlayLineClear()
    {
        PlayAudio(lineClear);
    }

    private void PlayAudio(AudioClip clip)
    {
        if (isMute)
            return;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void OnAudioButtonClick()
    {
        isMute = !isMute;
        ctrl.view.SetMuteActive(isMute);
        if (isMute == false)
            PlayCursor();
    }
}
