using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionDetailPanel : MonoBehaviour
{
    OptionButton[] optionButtons;

    private void Awake()
    {
        optionButtons = GetComponentsInChildren<OptionButton>(true);

     
    }

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
}
