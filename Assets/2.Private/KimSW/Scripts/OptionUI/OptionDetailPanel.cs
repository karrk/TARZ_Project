using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OptionDetailPanel : MonoBehaviour
{
    OptionButton[] optionButtons;

    [SerializeField] ISaveOption[] options;


    private void Awake()
    {
        optionButtons = GetComponentsInChildren<OptionButton>(true);
        options = GetComponentsInChildren<ISaveOption>(true);


    }
    /*
    private void OnEnable()
    {
        foreach (OptionButton button in optionButtons)
        {
            button.selectEvent += ChangeOptionDetail;
        }
    }
    private void OnDisable()
    {
        foreach (OptionButton button in optionButtons)
        {
            button.selectEvent -= ChangeOptionDetail;
        }
    }

    public void ChangeOptionDetail(GameObject obj)
    {
        foreach (OptionButton button in optionButtons)
        {
            button.set.SetActive(false);
        }

        obj.SetActive(true);
    }
    */
    public void SaveOption()
    {
        foreach(ISaveOption option in options)
        {
            option.SaveOption();
        }
       
    }
}
