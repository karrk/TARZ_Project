using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterSelectorActivator : MonoBehaviour
{
    public ChapterSelector chapterSelector;
    public GameObject interationUI;

    private bool isPlayerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            interationUI.SetActive(true);
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interationUI.SetActive(false);
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            chapterSelector.gameObject.SetActive(true);
            interationUI.SetActive(false);
        }
    }
}
