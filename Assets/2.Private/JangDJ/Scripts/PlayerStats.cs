using Zenject;

public class PlayerStats
{
    [Inject] private ProjectInstaller.PlayerBaseStats baseStats;

    // 장비 정보를 담는 클래스 객체
    // hp 는 캐릭터에서 한번 긁어간다.

    public float HP
    {
        get
        {
            return baseStats.Hp;
        }
    }

    public float Damage
    {
        get
        {
            return CalculateDamage();
        }
    }

    private float CalculateDamage()
    {
        
        // 장비의 각 옵션 수치를 계산한 값
        return baseStats.Atk;
    }
}

// 장비 상태 ?? 클래스

// ui에서 장비를 착용했을때, 이 클래스 내부에 업데이트시킨다.

// ui에서 장비가 교체될때, 이 클래스 내부에 업데이트 시킨다.

// 기획서없다, 간소화가 어느정도인지 모르겠다,
// 간단하게 hp , atk, def,
