// 전역적으로 활용되는 유저의 데이터 정보
public class PlayerData
{
    // 초안 예정요소 - 장비, 캐릭터 스탯, 재화, 스킬, 특수강화, 각 게이지 수치
    //                투사체 소지정보, 현재 스테이지정보(몇스테이지 인지)

    public int Gold { get; private set; }
    public float SkillGauge { get; private set; }
    public float UltimateGauge { get; private set; }
    public int LastStage { get; private set; }

    public void UpdateGold(int value) { this.Gold = value; }
    public void UpdateSkillGauge(float value) { this.SkillGauge = value; }
    public void UpdateUltimateGauge(float value) { this.UltimateGauge = value; }
    public void UpdateLastStage(int value) { this.LastStage = value; }
}