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
#if UNITY_EDITOR
        dropdown.value = changeNum;
#else
        for (int i = 0; i < resolutionX.Length; i++)
        {
            if (resolutionX[i] == Screen.currentResolution.width)
            {
                dropdown.value = i;
            }
        }
#endif



    }

    public void SaveOption()
    {
        
        Screen.SetResolution(resolutionX[tempNum], resolutionY[tempNum], true);
        changeNum = tempNum;
        
    }

    public void SetResolution(TMP_Dropdown change)
    {
        tempNum = change.value;
    }

}
