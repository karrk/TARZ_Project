using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class PlayerControllerHGH : MonoBehaviour
{
    public float playerCurHp;
    public float playerMaxHp;

    private void Awake()
    {
        playerMaxHp = 100;
        playerCurHp = 100;
    }

    public void PlayerDamaged()
    {
        playerCurHp -= 25;
    }

    public void RecoveryPlayerHp()
    {
        playerCurHp = playerMaxHp;
    }
}
