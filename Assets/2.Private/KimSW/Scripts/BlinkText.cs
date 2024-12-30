using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml;
using TMPro;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    Material textMaterial;

    [SerializeField] float dilate;
    [SerializeField] float duration;

    Sequence sequence;

    private void Awake()
    {
        textMaterial = GetComponent<TextMeshProUGUI>().font.material;

    }

    void OnEnable()
    {
        dilate = 0;
        sequence = DOTween.Sequence().
            Append(DOTween.To(() => dilate, x => dilate = x, 0.2f, duration)).
             OnUpdate(() => { textMaterial.SetFloat("_FaceDilate", dilate); }).
             SetLoops(-1,LoopType.Yoyo);
       
    }

    private void OnDisable()
    {
        sequence.Kill();
    }


}
