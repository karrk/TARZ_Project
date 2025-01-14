using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StageController : IInitializable
{
    [Inject] private PlayerStats playerStats;

    public void Initialize()
    {
        playerStats.SceneChangedFunction();
        
    }
}
