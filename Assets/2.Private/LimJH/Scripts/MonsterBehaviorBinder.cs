using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonsterBehaviorBinder : MonoBehaviour
{
    [Inject]
    [SerializeField] ProjectPlayer player; // Zenject로 주입받은 플레이어 객체

    private void Start()
    {
        // "Monster" 태그를 가진 모든 객체를 찾음
        var monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach (var monster in monsters)
        {
            var behaviorTree = monster.GetComponent<BehaviorTree>();
            if (behaviorTree != null)
            {
                behaviorTree.SetVariableValue("selfObject", monster.gameObject);

                // "targetObject"에 플레이어 설정
                behaviorTree.SetVariableValue("targetObject", player.gameObject);
            }
            else
            {
                Debug.LogWarning($"{monster.name}에서 BehaviorTree 컴포넌트를 찾을 수 없습니다!");
            }
        }
    }
}
