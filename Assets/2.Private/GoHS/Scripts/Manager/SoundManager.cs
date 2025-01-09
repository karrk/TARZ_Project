using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using Zenject;

public class SoundManager : MonoBehaviour
{
    [Inject] private ProjectInstaller.SoundSetting soundSetting;
    public ProjectInstaller.SoundSetting SoundSetting => soundSetting;

    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfx;

    public void PlayBGM(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.ignoreListenerPause = true;
        Debug.Log($"BGM 클립 이름 : {bgm.clip.name}");
        bgm.Play();

    }

    public void StopBGM(AudioClip clip)
    {
        if (!bgm.isPlaying == false)
        {
            return;
        }
        bgm.Stop();
    }

    public void StopCurBGM()
    {
        if (bgm.isPlaying)
        {
            bgm.Stop();
        }
    }

    public void PauseBGM()
    {
        if (bgm.isPlaying == false)
            return;
        bgm.Pause();
    }

    public void LoopBGM(bool loop)
    {
        bgm.loop = loop;
    }

    public void SetBGM(float volume, float pitch = 1f)
    {
        bgm.volume = volume;
        bgm.pitch = pitch;
    }

    public void PlaySFX(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }

    public void SetSFX(float volume, float pitch = 1f)
    {
        sfx.volume = volume;
        sfx.pitch = pitch;
    }
}

