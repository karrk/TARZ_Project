using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SoundOption : MonoBehaviour, ISaveOption
{
    [Inject]
    SoundManager soundManager;

    [SerializeField] Slider slider;

    public float soundValue;


    private void OnEnable()
    {
        slider.value = soundManager.GetVolume();
        soundValue = slider.value;
    }

    public void SetValue()
    {
        soundValue = slider.value;
    }

 
    public void SaveOption()
    {
        soundManager.SetBGM(soundValue);
        soundManager.SetSFX(soundValue);
    }
}
