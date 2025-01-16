using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SoundOption : MonoBehaviour
{
    [Inject]
    SoundManager soundManager;

    [SerializeField] Slider slider;

    public float soundValue;

    private void Awake()
    {
        slider.value = soundManager.GetVolume();
        soundValue = slider.value;
    }

    public void SetValue()
    {
        soundValue = slider.value;
    }

    public void SaveSoundValue()
    {
        soundManager.SetBGM(soundValue);
        soundManager.SetSFX(soundValue);
    }
}
