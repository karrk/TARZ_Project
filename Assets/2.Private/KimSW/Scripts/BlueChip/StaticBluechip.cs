using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class StaticBluechip : MonoBehaviour
{
    [Inject]
    PlayerStats stats;

    public bool[] bluechipCheck;
    public bool firstInit;
   
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetBlueChip();
    }

    public void SetBlueChip()
    {
        stats.UsedMeleePowerUp = false;
        stats.ExpMode = false;
        stats.ZeroGarbageMode = false;
        stats.AbsorbHpMode = false; 
        stats.SwitchBGM = false; 
   
        int num = -1;
        for (int i = 0; i < bluechipCheck.Length; i++)
        {
            if (bluechipCheck[i])
            {
                num = i;
            }
        }

        if (num > 0)
        {
            stats.AddBlueChip((BlueChipType)num);
        }
    }
}
