using UnityEngine;
using Zenject;

public class SoundManager : MonoBehaviour
{
    //[Inject] private ProjectInstaller.SoundSetting soundSetting;
    //public ProjectInstaller.SoundSetting SoundSetting => soundSetting;

    [Inject] private ProjectInstaller.AudioClips clips;

    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfx;
    [SerializeField] AudioSource skillSound;

    private E_Audio curBGM; 

    /// <summary>
    /// BGM 재생시 호출 하는 함수
    /// </summary>
    public void PlayBGM(E_Audio bgmType)
    {
        if (bgmType == E_Audio.None)
            StopBGM();

        bgm.clip = clips[bgmType];
        bgm.ignoreListenerPause = true;
        //Debug.Log($"BGM 클립 이름 : {bgm.clip.name}");
        bgm.Play();

    }

    /// <summary>
    /// BGM 멈출때 사용하는 함수
    /// </summary>
    public void StopBGM()
    {
        bgm.Stop();
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
    public void SetLoopBGM(bool loop)
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
    public void PlaySFX(E_Audio type)
    {
        sfx.PlayOneShot(clips[type]);
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

    public float GetVolume()
    {
        return bgm.volume;
    }

    public void SkillSoundStart(E_Audio type)
    {
        skillSound.clip = clips[type];
        skillSound.ignoreListenerPause = true;
        skillSound.Play();
    }

    public void SkillSoundStop()
    {
        skillSound.Stop();
    }

    public void SetLoopSKillSound(bool loop)
    {
        skillSound.loop = loop;
    }

    // 위치기반 SFX 음원 재생 
}

