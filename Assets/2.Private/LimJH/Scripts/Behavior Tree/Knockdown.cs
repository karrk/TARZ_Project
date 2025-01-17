using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Knockdown : BaseAction
{
    //public SharedVector3 playerVector;
    //public Animator animator;           // 애니메이터 참조
    //public float knockdownDuration = 2f; // 넉다운 상태 유지 시간
    //
    //public float pushDistance = 5f;         // 밀려날 거리
    //public float pushSpeed = 10f;            // 밀려나는 속도
    //public float maxDuration = 1f;          // 최대 지속 시간

    private Vector3 pushPos;         // 몬스터가 밀려날 목표 위치
    private float elapsedTime;              // 경과 시간
    private bool isKnockdown;   // 넉다운 상태 활성화 여부

    public override void OnStart()
    {
        // Animator 컴포넌트 가져오기
        //if (animator == null)
        //{
        //    animator = GetComponent<Animator>();
        //}

        base.OnStart();

        elapsedTime = 0;
        isKnockdown = true;

        // 플레이어 위치와 몬스터 위치를 기준으로 방향 계산
        Vector3 monsterPosition = transform.position;
        Vector3 direction = (monsterPosition - mob.SkillPos).normalized;

        // 목표 위치 계산
        pushPos = monsterPosition + direction * mob.Pushed.PushDist;

        // 넉다운 애니메이션 활성화
        mob.Reference.Anim.SetBool("isKnockDown", true);
    }

    public override TaskStatus OnUpdate()
    {
        elapsedTime += Time.deltaTime;

        if (isKnockdown)
        {
            // 몬스터를 목표 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, pushPos, 
                mob.Pushed.PushSpeed * Time.deltaTime);

            // 목표 지점에 도달하거나 밀리는 상태 시간이 끝났는지 확인
            if (Vector3.Distance(transform.position, pushPos) < 0.5f || 
                elapsedTime >= mob.Pushed.MaxDuration)
            {
                isKnockdown = false; // 넉다운 상태로 전환
                elapsedTime = 0;          // 타이머 리셋
            }
        }
        else
        {
            // 넉다운 상태 유지 시간 체크
            if (elapsedTime >= mob.Knock.DownDration)
            {
                // 넉다운 애니메이션 해제
                mob.Reference.Anim.SetBool("isKnockDown", false);
                return TaskStatus.Success; // 작업 완료
            }
        }

        return TaskStatus.Running; // 실행 중
    }
}