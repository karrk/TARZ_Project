using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class LongRangeSkill_1 : BaseState
{
    //[SerializeField] private ProjectPlayer player;

    //public LongRangeSkill_1(ProjectPlayer player)
    //{
    //    this.player = player;
    //    this.delay = 0.5f;
    //}

    public LongRangeSkill_1(ProjectPlayer player) : base(player)
    {
    }

    private GameObject armUnit => player.Refernece.Skill1_ArmUnit;
    private GameObject hitBox => player.Refernece.Skill1HitBox;

    [SerializeField] private float curDelay;

    public override void Enter()
    {
        //Debug.Log("스킬 1 시전 시작!");
        curDelay = player.Setting.Skill1Setting.Delay;

        CameraController cameraController = player.Cam.GetComponent<CameraController>();

        if (cameraController != null)
        {

            Vector3 cameraEuler = player.Cam.transform.rotation.eulerAngles;
            Vector3 playerEuler = player.transform.rotation.eulerAngles;

            player.transform.rotation = Quaternion.Euler(playerEuler.x, cameraEuler.y, cameraEuler.z);

        }

        armUnit.SetActive(true);
        player.SoundManager.PlaySFX(E_Audio.Char_ArmUnit);
        player.Refernece.EffectController.UseSkillEffect();
    }

    public override void Update()
    {
        //if(curDelay > 0f)
        //{
        //    curDelay -= Time.deltaTime;
            
        //}
        //else
        //{
        //    if (!hitBox.activeSelf)
        //    {
        //        hitBox.SetActive(true);
        //        Debug.Log("원거리 스킬 1 활성화됨");
        //        curDelay = player.Setting.Skill1Setting.Delay;
        //    }
        //    else
        //    {
        //        Debug.Log("딜레이 다 지나감");
        //        player.ChangeState(E_State.Idle);
        //    }
        //}
    }

    public override void Exit()
    {
        armUnit.SetActive(false);
        hitBox.SetActive(false);
    }

    public void LongRangeSkill_1_On()
    {
        player.StartCoroutine(DelayCoroutine());
        player.Refernece.EffectController.LongRangeSkill_1Effect();
    }
        
    private IEnumerator DelayCoroutine()
    {
        hitBox.SetActive(true);
        player.SoundManager.PlaySFX(E_Audio.Char_ArmUnit);
        yield return new WaitForSeconds(curDelay);
        player.ChangeState(E_State.Idle);
    }
}
