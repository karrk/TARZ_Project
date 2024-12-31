using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class LongRangeSkill_HitBox1 : MonoBehaviour
{
    [SerializeField] private float skillDamage;       // 스킬 공격력. 현재 기획서 상 100 데미지


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("적 부딪힘");             // 부딪히는 것 확인됨

        if(other.CompareTag("Monster"))
        {
            // TODO : 우선 생각되는 것은 몬스터 스크립트에 IDamagable 같은 인터페이스를 작성하여 상속시키게 하고,
            // 해당 인터페이스가 부딪힌 오브젝트에서 확인되면 데미지를 입게끔 만들면 되지 않을까?

            IDamagable damagable = other.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeHit(skillDamage, false);
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("적 부딪힘");
    //}


}
