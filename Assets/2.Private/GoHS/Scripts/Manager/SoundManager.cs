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


    /// <summary>
    /// BGM 재생시 호출 하는 함수
    /// </summary>
    /// <param name="clip"></param>
    public void PlayBGM(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.ignoreListenerPause = true;
        Debug.Log($"BGM 클립 이름 : {bgm.clip.name}");
        bgm.Play();

    }

    /// <summary>
    /// BGM 멈출때 사용하는 함수
    /// </summary>
    /// <param name="clip"></param>
    public void StopBGM(AudioClip clip)
    {
        if (!bgm.isPlaying == false)
        {
            return;
        }
        bgm.Stop();
    }


    /// <summary>
    /// BGM 현재 나오고있으면 멈추는 함수
    /// </summary>
    public void StopCurBGM()
    {
        if (bgm.isPlaying)
        {
            bgm.Stop();
        }
    }

    /// <summary>
    /// BGM 일시 정지 함수
    /// </summary>
    public void PauseBGM()
    {
        if (bgm.isPlaying == false)
            return;
        bgm.Pause();
    }

    /// <summary>
    /// BGM 반복재생할지 말지 여부 함수
    /// </summary>
    /// <param name="loop"></param>
    public void LoopBGM(bool loop)
    {
        bgm.loop = loop;
    }

    /// <summary>
    /// BGM의 볼륨과 비치를 설정하는 함수
    /// </summary>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    public void SetBGM(float volume, float pitch = 1f)
    {
        bgm.volume = volume;
        bgm.pitch = pitch;
    }

    /// <summary>
    /// 효과음을 재생시키는 함수
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySFX(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }

    /// <summary>
    /// 효과음의 볼륨과 피치를 설정하는 함수
    /// </summary>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    public void SetSFX(float volume, float pitch = 1f)
    {
        sfx.volume = volume;
        sfx.pitch = pitch;
    }
}

