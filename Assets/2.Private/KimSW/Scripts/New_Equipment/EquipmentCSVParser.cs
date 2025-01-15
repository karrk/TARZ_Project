using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class EquipmentCSVParser : MonoBehaviour
{

    public List<NewEquipment> newEquipments = new List<NewEquipment>();

    //[SerializeField]
    //TextAsset csv;

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

//        if (File.Exists($"{path}/EquipmentDatatable.csv") == false)
//        {
//            Debug.LogError("파일이 없습니다");
//            return;
//        }

//        string file = File.ReadAllText($"{path}/EquipmentDatatable.csv");
//        string[] lines = file.Split('\n');

        TextAsset asset = Resources.Load<TextAsset>("EquipmentDatatable");

        string[] lines = asset.text.Split('\n');

        for (int y = 3; y < lines.Length; y++)
        {
            string[] values = lines[y].Split(',', '\t');
          

            NewEquipment newEquipment = ScriptableObject.CreateInstance<NewEquipment>();
            newEquipment.id = int.Parse(values[0]);
            newEquipment.equipmentName = values[1];

            switch (values[2])
            {
                case "노말":
                    newEquipment.rarityTier = RarityTier.COMMON;
                    break;
                case "레어":
                    newEquipment.rarityTier = RarityTier.RARE;
                    break;
                case "유니크":
                    newEquipment.rarityTier = RarityTier.UNIQUE;
                    break;
                case "레전더리":
                    newEquipment.rarityTier = RarityTier.LEGENDARY;
                    break;
            }

            newEquipment.upgradeLevel = int.Parse(values[3]);


            switch (values[4])
            {
                case "ATK":
                    newEquipment.optionType = OptionType.ATK;
                    break;
                case "MOVESPD":
                    newEquipment.optionType = OptionType.MOVESPD;
                    break;
                case "ATKSPD":
                    newEquipment.optionType = OptionType.ATKSPD;
                    break;
                case "INVENTORY":
                    newEquipment.optionType = OptionType.INVENTORY;
                    break;
                case "HP":
                    newEquipment.optionType = OptionType.HP;
                    break;
                case "LUCK":
                    newEquipment.optionType = OptionType.LUCK;
                    break;
                case "GAUGEINC":
                    newEquipment.optionType = OptionType.GAUGEINC;
                    break;
                case "STAMINA":
                    newEquipment.optionType = OptionType.STAMINA;
                    break;
                default:
                    newEquipment.optionType = OptionType.SIZE;
                    break;
            }


            newEquipment.optionValue = int.Parse(values[5]);


            switch (values[6])
            {
                case "BOOTS":
                    newEquipment.equipmentType = EquipmentType.BOOTS;
                    break;
                case "ARM":
                    newEquipment.equipmentType = EquipmentType.ARM;
                    break;
                case "EARRING":
                    newEquipment.equipmentType = EquipmentType.EARRING;
                    break;
                case "RING":
                    newEquipment.equipmentType = EquipmentType.RING;
                    break;
                case "NECKLACE":
                    newEquipment.equipmentType = EquipmentType.NECKLACE;
                    break;
                case "LEG":
                    newEquipment.equipmentType = EquipmentType.LEG;
                    break;
                case "CHEST":
                    newEquipment.equipmentType = EquipmentType.CHEST;
                    break;
                case "BACKPACK":
                    newEquipment.equipmentType = EquipmentType.BACKPACK;
                    break;
                default:
                    newEquipment.equipmentType = EquipmentType.SIZE;
                    break;
            }
           
            newEquipment.illust = Addressables.LoadAssetAsync<Sprite>(values[7]).WaitForCompletion();

            newEquipment.description = values[8];

            newEquipments.Add(newEquipment);

            // AssetDatabase.CreateAsset(newEquipment, $"Assets/2.Private/KimSW/Prefabs/Required/InGame/NewEquipmentSO/{newEquipment.id}.asset");
        }


    }
}
