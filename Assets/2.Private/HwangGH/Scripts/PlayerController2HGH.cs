using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2HGH : MonoBehaviour
{
    public float playerHp;

    private void Awake()
    {
        playerHp = 100f;
    }

    public void PlayerDead()
    {
        if(playerHp <= 0)
        {
            Debug.Log($"{playerHp} 플레이어 죽음");
        }
        
    }
}
