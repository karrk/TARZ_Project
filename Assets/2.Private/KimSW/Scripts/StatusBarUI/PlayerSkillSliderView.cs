using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using Zenject;

public class PlayerSkillSliderView : SliderView
{
    [SerializeField] GameObject markerPrefab;
    [SerializeField] RectTransform markerTransform;

    [Inject]
    ProjectInstaller.PlayerSettings settings;

    void Start()
    {
        SetSliderMax(100);
        SetGaugePointCheck();



    }

    /// <summary>
    /// 스킬 게이지 구간 체크 세팅
    /// </summary>
    public void SetGaugePointCheck()
    {
        float temp = slider.value;

        for (int i = 0; i < settings.BasicSetting.SkillAnchor.Length; i++)
        {
            slider.value = slider.maxValue * settings.BasicSetting.SkillAnchor[i];
            Instantiate(markerPrefab, markerTransform.position, Quaternion.identity).transform.SetParent(slider.transform);

        }

        slider.value = temp;


    }


}
