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

        for(int i = 0; i< settings.SkillAnchor.Length; i++)
        {
            slider.value = slider.maxValue * settings.SkillAnchor[i];
            Instantiate(markerPrefab, markerTransform.position, Quaternion.identity).transform.SetParent(slider.transform); 
            
        }

      
        
    }

   
}
