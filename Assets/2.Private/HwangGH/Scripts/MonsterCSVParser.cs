using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


public class MonsterCSVParser : MonoBehaviour
{
    private void Awake()
    {
        // 1. 폴더가 있는지 없는지 체크

        // 게임이 있는 경로
        string path = $"{Application.dataPath}/2.Private\\HwangGH\\MonsterDatatables";
        // 경로가 잘못됐으면 잘못됐다고 출력
        if(Directory.Exists(path) == false)
        {
            Debug.LogError("경로가 없습니다.");
            return;
        }
        // 파일이 없으면 출력되는 에러
        if(File.Exists($"{path}/MonsterDatatables.csv") == false)
        {
            Debug.LogError("파일이 없습니다.");
            return;
        }

        string monsterFile = File.ReadAllText($"{path}/MonsterDatatables.csv");

        string[] lines = monsterFile.Split('\n');
        for(int y = 1; y < lines.Length; y++)
        {
            MonsterScriptableObject monsterData = ScriptableObject.CreateInstance<MonsterScriptableObject>();

            string[] values = lines[y].Split(',', '\t');

            monsterData.monsterName = values[0];
            monsterData.monsterHp = float.Parse(values[1]);
            monsterData.monsterPower = float.Parse(values[2]);
            monsterData.attackSpeed = float.Parse(values[3]);
            monsterData.MoveSpeed = float.Parse(values[4]);
            monsterData.ProjectileSpeed = float.Parse(values[5]);
            monsterData.StatusResistance = float.Parse(values[6]);
            monsterData.HitResistance = float.Parse(values[7]);
            monsterData.AttackRange = float.Parse(values[8]);
            monsterData.DetectionRange = float.Parse(values[9]);
            monsterData.SkillConditionTime = float.Parse(values[10]);
            monsterData.SkillConditionCount = float.Parse(values[11]);
            monsterData.SkillConditionRange = float.Parse(values[12]);
            monsterData.GroggyTime = float.Parse(values[13]);
            monsterData.GimmickBreakHitCount = float.Parse(values[14]);
            monsterData.Skill1CastProbability = float.Parse(values[15]);
            monsterData.Skill2CastProbability = float.Parse(values[16]);
            monsterData.Skill3CastProbability = float.Parse(values[17]);
            monsterData.BerserkSkillConditionCount = float.Parse(values[18]);
            monsterData.BerserkAttackSpeed = float.Parse(values[19]);
            monsterData.BerserkSkill1CastProbability = float.Parse(values[20]);
            monsterData.BerserkSkill2CastProbability = float.Parse(values[21]);
            monsterData.BerserkSkill3CastProbability = float.Parse(values[22]);

            AssetDatabase.CreateAsset(monsterData, $"Assets/3.Private/HwangGH/MonsterDatatables");
        }

       
    }
}
