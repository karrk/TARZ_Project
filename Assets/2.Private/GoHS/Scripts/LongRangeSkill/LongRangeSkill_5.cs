using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

[Serializable]
public class LongRangeSkill_5 : BaseState
{
    [Inject] private CoroutineHelper helper;
    [Inject] private Shooter shooter;

    private GameObject garbages => player.Refernece.Skill5Garbages;

    private int throwCount;
    private float startDelay;
    private float endDelay;
    private float throwPower;
    private float rotateSpeed;
    private float rotateTime;
    private float radius;

    public LongRangeSkill_5(ProjectPlayer player) : base(player)
    {
    }

    public float Radius => radius;
    public Vector3 AnchorPos => garbages.transform.position;

    public override void Enter()
    {
        helper.StartCoroutine(ActionSkill());
    }

    private IEnumerator ActionSkill()
    {
        float rotateTimer = 0;

        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            if(rotateTime <= rotateTimer) { break; }

            Rotation();

            rotateTimer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(endDelay);

        Shoot();

        player.ChangeState(E_State.Idle);
    }

    private void Rotation()
    {
        garbages.transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.Self);
    }

    private void Shoot()
    {
        Vector3 randPos = GetRandomPos();
        Vector3 dir = (AnchorPos - randPos).normalized;

        for (int i = 0; i < throwCount; i++)
        {
            shooter.FireItem(randPos, dir);
        }
    }

    private Vector3 GetRandomPos()
    {
        Vector3 randDir = new Vector3(
            Random.Range(-1f, 1f),
            0,
            Random.Range(-1f, 1f));

        randDir = randDir.normalized;

        Vector3 newPos = AnchorPos + randDir * radius;

        return newPos;
    }

}
