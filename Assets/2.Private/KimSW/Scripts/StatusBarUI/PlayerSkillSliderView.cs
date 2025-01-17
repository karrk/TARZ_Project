using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using Zenject;

public class PlayerSkillSliderView : SliderView
{
    [SerializeField] GameObject markerPrefab;
    [SerializeField] RectTransform markerTransform;
    [SerializeField] TMP_Text skillCountText;
    [SerializeField] GameObject effect;

    [Inject]
    ProjectInstaller.PlayerSettings settings;

    StringBuilder sb = new StringBuilder();
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

    public override void SetSlider(float value)
    {
       
        slider.value = value;
        sb.Clear();

        int count = 0;
      
       
        for (int i = 0; i < settings.BasicSetting.SkillAnchor.Length; i++)
        {
            if ((int)value >= (int)(settings.BasicSetting.SkillAnchor[i]*100))
            {  
                count++;
            }
           

        }

        sb.Append("Skill ");
        sb.Append(count);

        skillCountText.text = sb.ToString();

        if(count < 1)
        {
            effect.SetActive(false);
        }
        else
        {
            effect.SetActive(true);
        }
    }
}
