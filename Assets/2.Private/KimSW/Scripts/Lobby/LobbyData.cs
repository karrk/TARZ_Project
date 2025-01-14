using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LobbyData : MonoBehaviour
{
    [Inject]
    PlayerUIModel model;

    public int exp;
    public bool[] passiveEnable;

    void Start()
    {
        SetExp();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
   

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetExp();
    }

    public void SetExp()
    {
        model.TargetEXP.Value = exp;
    }
   
}
