using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class LongRangeSkill_5 : BaseState
{
    private GameObject garbages => player.Refernece.Skill5Garbages;

    public LongRangeSkill_5(ProjectPlayer player) : base(player)
    {
    }

    public float Radius => player.Setting.Skill5Setting.Radius;
    public Vector3 AnchorPos => garbages.transform.position;

    public override void Enter()
    {
        player.StartCoroutine(ActionSkill());
    }

    private IEnumerator ActionSkill()
    {
        float rotateTimer = 0;

        yield return new WaitForSeconds(player.Setting.Skill5Setting.StartDelay);
        garbages.SetActive(true);

        while (true)
        {
            if(player.Setting.Skill5Setting.RotateTime <= rotateTimer) { break; }

            Rotation();

            rotateTimer += Time.deltaTime;
            yield return null;
        }

        garbages.SetActive(false);
        Shoot();
        yield return new WaitForSeconds(player.Setting.Skill5Setting.EndDelay);

        player.ChangeState(E_State.Idle);
    }

    private void Rotation()
    {
        garbages.transform.Rotate(Vector3.up * Time.deltaTime * player.Setting.Skill5Setting.RotateSpeed, Space.Self);
    }

    private void Shoot()
    {
        Vector3 randPos;
        Vector3 dir;

        for (int i = 0; i < player.Setting.Skill5Setting.ThrowCount; i++)
        {
            randPos = GetRandomPos();
            dir = (randPos - AnchorPos).normalized;
            player.Refernece.Shooter.FireItem(randPos, dir);
        }
    }

    private Vector3 GetRandomPos()
    {
        Vector3 randDir = new Vector3(
            Random.Range(-1f, 1f),
            0,
            Random.Range(-1f, 1f));

        randDir = randDir.normalized;

        Vector3 newPos = AnchorPos + randDir * player.Setting.Skill5Setting.Radius;

        return newPos;
    }

}
