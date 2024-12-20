using UnityEngine;
using UnityEditor;
using System.IO;
using DG.Tweening.Plugins.Core.PathCore;


public class CSVtoSO
{
    private static string monsterCSVPath = Application.dataPath + "\\2.Private\\HwangGH\\MonsterDatatables";

    [MenuItem("Utilities/Generate MonsterSO")]
    public static void GenerateMonsterSO()
    {
        if (Directory.Exists($"{monsterCSVPath}") == false)
        {
            Debug.LogError("경로가 없습니다.");
            return;
        }

        // 파일이 없으면 출력되는 에러
        if (File.Exists($"{monsterCSVPath}/MonsterDatatables.csv") == false)
        {
            Debug.LogError("파일이 없습니다.");
            return;
        }
        string[] allLines = File.ReadAllLines(monsterCSVPath + "/MonsterDatatables.csv");

        for (int y = 1; y < allLines.Length; y++)
        {
            string[] values = allLines[y].Split(',','\t');

            if (values.Length != 13)
            {
                Debug.Log(allLines + " Does not have 13 value");
            }

            MonsterScriptableObject monsterSO = ScriptableObject.CreateInstance<MonsterScriptableObject>();
            monsterSO.monsterName = values[0];
            monsterSO.monsterHp = int.Parse(values[1]);
            monsterSO.monsterPower = float.Parse(values[2]);
            monsterSO.attackSpeed = float.Parse(values[3]);
            monsterSO.MoveSpeed = float.Parse(values[4]);
            monsterSO.ProjectileSpeed = float.Parse(values[5]);
            monsterSO.StatusResistance = float.Parse(values[6]);
            monsterSO.HitResistance = float.Parse(values[7]);
            monsterSO.AttackRange = float.Parse(values[8]);
            monsterSO.DetectionRange = float.Parse(values[9]);
            monsterSO.SkillConditionTime = float.Parse(values[10]);
            monsterSO.SkillConditionCount = float.Parse(values[11]);
            monsterSO.SkillConditionRange = float.Parse(values[12]);
            monsterSO.GroggyTime = float.Parse(values[13]);
            monsterSO.GimmickBreakHitCount = float.Parse(values[14]);
            monsterSO.Skill1CastProbability = float.Parse(values[15]);
            monsterSO.Skill2CastProbability = float.Parse(values[16]);
            monsterSO.Skill3CastProbability = float.Parse(values[17]);
            monsterSO.BerserkSkillConditionCount = float.Parse(values[18]);
            monsterSO.BerserkAttackSpeed = float.Parse(values[19]);
            monsterSO.BerserkSkill1CastProbability = float.Parse(values[20]);
            monsterSO.BerserkSkill2CastProbability = float.Parse(values[21]);
            monsterSO.BerserkSkill3CastProbability = float.Parse(values[22]);

            AssetDatabase.CreateAsset(monsterSO, $"Assets/2.Private/HwangGH/MonsterDatatables/{monsterSO.monsterName}.asset");
        }

        AssetDatabase.SaveAssets();
    }
}