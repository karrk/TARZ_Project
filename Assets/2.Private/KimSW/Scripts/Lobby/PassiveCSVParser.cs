using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
public class PassiveInfo
{
    public int id;
    public string statName;
    public OptionType statType;
    public float statValue;
    public int cost;
    public Sprite illust;
}

public class PassiveCSVParser : MonoBehaviour
{
    [Inject]
    LobbyData lobbyData;

    public List<PassiveInfo> passives = new List<PassiveInfo>();

    private void Awake()
    {
//#if UNITY_EDITOR
//        string path = $"{Application.dataPath}/2.Private/KimSW/CSV";
//#else
//                string path = Application.persistentDataPath; 
//#endif
//        if (Directory.Exists(path) == false)
//        {
//            Debug.LogError("경로가 없습니다");
//            return;
//        }

//        if (File.Exists($"{path}/StatDatatable.csv") == false)
//        {
//            Debug.LogError("파일이 없습니다");
//            return;
//        }

//        string file = File.ReadAllText($"{path}/StatDatatable.csv");
//        string[] lines = file.Split('\n');

        TextAsset asset = Resources.Load<TextAsset>("StatDatatable");

        string[] lines = asset.text.Split('\n');

        for (int y = 1; y < lines.Length; y++)
        {
            string[] values = lines[y].Split(',', '\t');


            PassiveInfo passive = new PassiveInfo();
            passive.id = int.Parse(values[0]);

            passive.statName = values[1];

          
            switch (values[2])
            {
                case "ATK":
                    passive.statType = OptionType.ATK;
                    break;
                case "MOVESPD":
                    passive.statType = OptionType.MOVESPD;
                    break;
                case "ATKSPD":
                    passive.statType = OptionType.ATKSPD;
                    break;
                case "INVENTORY":
                    passive.statType = OptionType.INVENTORY;
                    break;
                case "HP":
                    passive.statType = OptionType.HP;
                    break;
                case "LUCK":
                    passive.statType = OptionType.LUCK;
                    break;
                case "GAUGEINC":
                    passive.statType = OptionType.GAUGEINC;
                    break;
                case "STAMINA":
                    passive.statType = OptionType.STAMINA;
                    break;
                default:
                    passive.statType = OptionType.SIZE;
                    break;
            }


            passive.statValue = float.Parse(values[3]);
           

            passive.illust = Addressables.LoadAssetAsync<Sprite>(values[4]).WaitForCompletion();


            passive.cost = int.Parse(values[5]);

            passives.Add(passive);

         

            // AssetDatabase.CreateAsset(newEquipment, $"Assets/2.Private/KimSW/Prefabs/Required/InGame/NewEquipmentSO/{newEquipment.id}.asset");
        }
        lobbyData.passives = passives;

    }
}
