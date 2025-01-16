using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ResolutionOption : MonoBehaviour, ISaveOption
{
    [SerializeField] int[] resolutionX;
    [SerializeField] int[] resolutionY;
    [SerializeField] TMP_Dropdown dropdown;

    int tempNum;
    int changeNum;

    private void OnEnable()
    {
       dropdown.value = changeNum;
    }

    public void SaveOption()
    {
        Screen.SetResolution(resolutionX[tempNum], resolutionY[tempNum], false);
        changeNum = tempNum;
        
    }

    public void SetResolution(TMP_Dropdown change)
    {
        tempNum = change.value;
    }

}
