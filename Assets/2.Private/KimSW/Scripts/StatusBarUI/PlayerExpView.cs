using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerExpView : MonoBehaviour
{
   

    [SerializeField] TMP_Text expText;
 
    private void Awake()
    {
        expText = GetComponentInChildren<TMP_Text>(); 
    }

 

    public void SetExpText(int value)
    {
        expText.text = value.ToString("N0");
    }
}
