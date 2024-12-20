using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BindTest : MonoBehaviour
{
    [Inject]
    private PlayerUIModel model;

    [Inject]
    private GarbageQueue garbage;

    private void Start()
    {
        model.GarbageCount.Value = garbage.Count;
        garbage.ChangedInventory += () => { model.GarbageCount.Value = garbage.Count; };
    }

}
